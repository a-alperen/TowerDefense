using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Stats")]
    public Data data;
    public float gameMoney;
    [SerializeField] private float time;
    public int hitCount;
    public bool isPlaying;
    public bool isPaused;
    public int correctCount;
    public int wrongCount;
    public int gold;
    public int score;
    
    private DatabaseConnection connection;
    private bool isTimerRunning = false;

    //public GameObject winPanel;

    private void Awake()
    {
        Instance = this;
        isPaused = false;
        isPlaying = true;
        correctCount = 0;
        wrongCount = 0;
        gold = 0;
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        data = new Data();
        connection = GetComponent<DatabaseConnection>();
        StartCoroutine(connection.GetUserData(PlayerPrefs.GetString("username"), data));
        UISystem.Instance.UpdateUIText(gameMoney, time, correctCount, wrongCount, score, gold);
        UISystem.Instance.CloseGameOverPanel();
        //StartCoroutine("EarnGameMoney");
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UISystem.Instance.UpdateUIText(gameMoney, time, correctCount, wrongCount, score, gold);
        GameStatus();
        
    }

    private void GameStatus()// Oyunun duraklatilmasini kontrol eder.
    {
        if (isPaused) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void WinGame()// Oyunu kazandiginda calisir.
    {
        StartCoroutine(Win());

        IEnumerator Win()
        {
            ClearMap();
            isPlaying = false;
            isPaused = true;
            StopAllCoroutines();
            SaveGameData(data);
            UISystem.Instance.ShowGameOverPanel("OYUNU KAZANDIN", score, gold, correctCount, wrongCount);
            yield return null;
        }
    }
    
    public void GameOver()// Oyunu kaybettiginde calisir.
    {
        StartCoroutine(Loose());

        IEnumerator Loose()
        {
            ClearMap();
            isPlaying = false;
            isPaused = true;
            StopAllCoroutines();
            SaveGameData(data);
            UISystem.Instance.ShowGameOverPanel("OYUNU KAYBETTİN", score, gold, correctCount, wrongCount);
            yield return null;
        }
    }

    public void RestartGame()// Oyunu yeniden baslatir.
    {
        StartCoroutine(Restart());

        IEnumerator Restart()
        {
            ClearMap();
            yield return StartCoroutine(connection.SetData(PlayerPrefs.GetString("username"), data));
            yield return SceneManager.LoadSceneAsync("Game");

        }
    }
    public void BackToMainMenu()// Ana menuye doner.
    {
        StartCoroutine(LoadMenu());

        IEnumerator LoadMenu()
        {
            isPlaying = false;
            ClearMap();
            yield return StartCoroutine(connection.SetData(PlayerPrefs.GetString("username"), data));
            yield return SceneManager.LoadSceneAsync("Menu");
        }
    }
    IEnumerator TimerCoroutine()
    {
        isTimerRunning = true;

        while (0 < time)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        isTimerRunning = false;
        GameOver();
    }

    void StartTimer()
    {
        if (!isTimerRunning)
        {
            StartCoroutine(TimerCoroutine());
        }
    }

    private void SaveGameData(Data data)
    {
        if (data.highGold < gold) data.highGold = gold;
        if (data.highScore < score) data.highScore = score;
        data.totalGold += gold;
        data.totalScore += score;
        data.marketGold += gold;

        switch (LectureManager.lecture)
        {
            case "Hayat Bilgisi":
                data.correctAnswers[0] += correctCount;
                data.wrongAnswers[0] += wrongCount;
                break;
            case "Türkçe":
                data.correctAnswers[1] += correctCount;
                data.wrongAnswers[1] += wrongCount;
                break;
            case "Matematik":
                data.correctAnswers[2] += correctCount;
                data.wrongAnswers[2] += wrongCount;
                break;
            default:
                break;
        }
    }
    IEnumerator EarnGameMoney()// Saniyede bir oyun ekranindaki yildiz parasini arttirir.
    {
        
        while (true)
        {
            gameMoney += 5;
            yield return new WaitForSeconds(1f);
        }
    }

    public void DestroyStudent()// Oyun bittikten sonra ogrencileri temizler.
    {
        for (int i = 0; i < Students.students.Count; i++)
        {
            Destroy(Students.students[i]);
        }
    }

    public void PauseGame()// Oyunun durdurur.
    {
        isPaused = true;
        UISystem.Instance.ShowPausePanel();
    }

    public void ResumeGame()// Oyunu devam ettirir.
    {
        isPaused = false;
        UISystem.Instance.ClosePausePanel();
    }

    private void ClearMap()// Haritayi temizler.
    {
        MapGenerator.mapTiles.Clear();
        MapGenerator.pathTiles.Clear();
        Students.students.Clear();
        DestroyStudent();
        Towers.towers.Clear();
        MapGenerator.startTile = null;
        MapGenerator.endTile = null;
    }

}
