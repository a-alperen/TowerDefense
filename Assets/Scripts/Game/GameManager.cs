using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float gameMoney;
    [SerializeField] private int healthCount;
    [SerializeField] private int enemyHealthCount;
    [SerializeField] private float time;

    public static bool isPlaying;

    private QuestionManager questionManager;
    private UISystem uiManager;
    
    //public GameObject winPanel;

    
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;

        uiManager = GetComponent<UISystem>();

        //questionManager = GameObject.Find("QuestionManager").GetComponent<QuestionManager>();
        //questionManager.AskLecture();
        ResumeGame();
        uiManager.UpdateUIText(gameMoney, healthCount, time);
        uiManager.CloseGameOverPanel();
        StartCoroutine("EarnGameMoney");
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.UpdateUIText(gameMoney,healthCount,time);
        if (time >= 0) 
        {
            Timer();
        }
        
    }
    #region Oyun Bilesenlerine Erisim

    /// <summary>
    /// Oyun parasini dondurur.
    /// </summary>
    /// <returns></returns>
    public float GetGameMoney()
    {
        return gameMoney;
    }
    /// <summary>
    /// Oyun parasini gunceller.
    /// </summary>
    /// <param name="money"></param>
    public void SetGameMoney(float money)
    {
        gameMoney= money;
    }
    /// <summary>
    /// Oyuncunun can degerini dondurur.
    /// </summary>
    /// <returns></returns>
    public int GetHealthCount()
    {
        return healthCount;
    }
    /// <summary>
    /// Oyuncun can degerini gunceller.
    /// </summary>
    /// <param name="health"></param>
    public void SetHealthCount(int health)
    {
        healthCount = health;
    }
    /// <summary>
    /// Dusmanin can degerini getirir.
    /// </summary>
    /// <returns></returns>
    public int GetEnemyHealthCount()
    {
        return enemyHealthCount;
    }
    /// <summary>
    /// Dusman canini gunceller.
    /// </summary>
    /// <param name="enemyHealth"></param>
    public void SetEnemyHealthCount(int enemyHealth)
    {
        enemyHealthCount = enemyHealth;
    }
    
    #endregion

    /// <summary>
    /// Oyuncunun canini azaltir.
    /// </summary>
    public void DecreasePlayerHealth()
    {
        healthCount -= 1;
        if(healthCount == 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Dusmanin canini azaltir.
    /// </summary>
    public void DecreaseEnemyHealth()
    {
        enemyHealthCount -= 1;
        if(enemyHealthCount == 0)
        {
            WinTheGame();
        }
    }

    /// <summary>
    /// Oyunu kazandiginda calisir.
    /// </summary>
    public void WinTheGame()
    {
        Time.timeScale = 0;
        StopAllCoroutines();
        DestroyStudent();
        uiManager.ShowGameOverPanel();
        Debug.Log("you win");
    }
    /// <summary>
    /// Oyunu kaybettiginde calisir.
    /// </summary>
    public void GameOver()
    {
        StopAllCoroutines();
        DestroyStudent();
        uiManager.ShowGameOverPanel();
        //PauseGame();
        Debug.Log("you loose");
    }

    /// <summary>
    /// Oyunu yeniden baslatir.
    /// </summary>
    public void RestartGame()
    {
        DestroyStudent();
        ClearMap();
        SceneManager.LoadScene("Game");
    }
    
    /// <summary>
    /// Oyundaki geri sayimi kontrol eder.
    /// </summary>
    private void Timer()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameOver();
        }
    }
    
    /// <summary>
    /// Saniyede bir oyun ekranindaki yildiz parasini arttirir.
    /// </summary>
    /// <returns></returns>
    IEnumerator EarnGameMoney()
    {
        
        while (true)
        {
            gameMoney += 5;
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Ogrenci maliyeti hesaplar.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public float StudentCost(GameObject gameObject)
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

    /// <summary>
    /// Ana menuye doner.
    /// </summary>
    public void BackToMainMenu()
    {
        //DestroyStudent();

        isPlaying = false;
        ClearMap();
        SceneManager.LoadScene("Menu");
    }
    
    /// <summary>
    /// Oyun bittikten sonra ogrencileri temizler.
    /// </summary>
    public void DestroyStudent()
    {
        for (int i = 0; i < Students.students.Count; i++)
        {
            Destroy(Students.students[i]);
        }
    }

    /// <summary>
    /// Oyunun durdurur.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        uiManager.ShowPausePanel();
    }

    /// <summary>
    /// Oyunu devam ettirir.
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1;
        uiManager.ClosePausePanel();
    }

    /// <summary>
    /// Haritayi temizler.
    /// </summary>
    private void ClearMap()
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
