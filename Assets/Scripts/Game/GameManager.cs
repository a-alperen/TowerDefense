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
    
    private const string dataFileName = "PlayerStats";

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
        data = SaveSystem.LoadData<Data>(dataFileName);

        UISystem.Instance.UpdateUIText(gameMoney, time, correctCount, wrongCount, score, gold);
        UISystem.Instance.CloseGameOverPanel();
        //StartCoroutine("EarnGameMoney");
    }

    // Update is called once per frame
    void Update()
    {
        UISystem.Instance.UpdateUIText(gameMoney, time, correctCount, wrongCount, score, gold);
        GameStatus();
        Timer();
        
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSystem.SaveData(data,dataFileName);
        }
    }
    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(data, dataFileName);
    }
    private void GameStatus()// Oyunun duraklatilmasini kontrol eder.
    {
        if (isPaused) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void WinGame()// Oyunu kazandiginda calisir.
    {
        SaveGameData();
        isPaused = true;
        isPlaying = false;
        StopAllCoroutines();
        DestroyStudent();
        UISystem.Instance.ShowGameOverPanel("OYUNU KAZANDIN",score,gold,correctCount,wrongCount);
        Debug.Log("you win");
    }
    
    public void GameOver()// Oyunu kaybettiginde calisir.
    {
        SaveGameData();
        isPaused =true;
        isPlaying = false;
        StopAllCoroutines();
        DestroyStudent();
        UISystem.Instance.ShowGameOverPanel("OYUNU KAYBETTİN",score,gold,correctCount,wrongCount);
        Debug.Log("you loose");
    }

    public void RestartGame()// Oyunu yeniden baslatir.
    {
        SaveSystem.SaveData(data, dataFileName);
        DestroyStudent();
        ClearMap();
        SceneManager.LoadScene("Game");
    }
    
    private void Timer()// Oyundaki geri sayimi kontrol eder.
    {
        
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        else GameOver();
    }
    
    private void SaveGameData()
    {
        if (data.highGold < gold) data.highGold = gold;
        if (data.highScore < score) data.highScore = score;
        data.totalGold += gold;
        data.totalScore += score;
        data.marketGold += gold;

        switch (LectureManager.lecture)
        {
            case "hayatbilgisi":
                data.correctAnswers[0] += correctCount;
                data.wrongAnswers[0] += wrongCount;
                break;
            case "turkce":
                data.correctAnswers[1] += correctCount;
                data.wrongAnswers[1] += wrongCount;
                break;
            case "matematik":
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

    public float StudentCost(GameObject gameObject)// Öğrenci maliyeti hesaplar ve değerini döndürür.
    {
        float cost = 0;
        Debug.Log(gameObject.name);
        if (gameObject.name == "NormalStudent")
        {
            cost = 25;
        }
        else if(gameObject.name == "FastStudent")
        {
            cost = 50;
        }
        else if (gameObject.name == "TankStudent")
        {
            cost= 100;
        }
        return cost;
    }

    public void BackToMainMenu()// Ana menuye doner.
    {
        SaveSystem.SaveData(data, dataFileName);
        isPlaying = false;
        ClearMap();
        SceneManager.LoadScene("Menu");
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
