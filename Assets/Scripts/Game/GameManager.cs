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
    public float gameMoney;
    [SerializeField] private float time;

    public bool isPlaying;
    public bool isPaused;

    //public GameObject winPanel;

    private void Awake()
    {
        Instance = this;
        isPaused = false;
        isPlaying = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        UISystem.Instance.UpdateUIText(gameMoney, time);
        UISystem.Instance.CloseGameOverPanel();
        //StartCoroutine("EarnGameMoney");
    }

    // Update is called once per frame
    void Update()
    {
        UISystem.Instance.UpdateUIText(gameMoney,time);
        if (time >= 0) 
        {
            Timer();
        }
        GameStatus();
    }

    private void GameStatus()// Oyunun duraklatilmasini kontrol eder.
    {
        if (isPaused) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public void WinGame()// Oyunu kazandiginda calisir.
    {
        isPaused = true;
        isPlaying = false;
        StopAllCoroutines();
        DestroyStudent();
        UISystem.Instance.ShowGameOverPanel();
        Debug.Log("you win");
    }
    
    public void GameOver()// Oyunu kaybettiginde calisir.
    {
        isPaused =true;
        isPlaying = false;
        StopAllCoroutines();
        DestroyStudent();
        UISystem.Instance.ShowGameOverPanel();
        Debug.Log("you loose");
    }

    public void RestartGame()// Oyunu yeniden baslatir.
    {
        DestroyStudent();
        ClearMap();
        SceneManager.LoadScene("Game");
    }
    
    private void Timer()// Oyundaki geri sayimi kontrol eder.
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameOver();
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
        if (gameObject.name == "Student")
        {
            cost = 25;
        }
        else if(gameObject.name == "LazyStudent")
        {
            cost = 50;
        }
        else if (gameObject.name == "HardworkingStudent")
        {
            cost= 100;
        }
        return cost;
    }

    public void BackToMainMenu()// Ana menuye doner.
    {
        //DestroyStudent();

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
