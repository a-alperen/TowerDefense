using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HighScoreManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] highScoresPrefabs;
    [SerializeField] private TextMeshProUGUI[] usernameTexts;
    [SerializeField] private TextMeshProUGUI[] highScoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(GetHighScores(60f));
    }
    
    public IEnumerator GetHighScores(float repeatTime)
    {
        while (true)
        {
            yield return HighScores();
            yield return new WaitForSeconds(repeatTime);
        }
        
    }
    private IEnumerator HighScores()
    {
        string url = "http://localhost/TowerDefense/HighScore.php";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonData = www.downloadHandler.text;
                HighScoreData[] highScores = JsonConvert.DeserializeObject<HighScoreData[]>(jsonData);
                UpdateHighScores(highScores);
                Debug.Log("Yüksek skor tablosu güncellendi.");
            }
            else
            {
                Debug.LogError("HTTP hata: " + www.error);
            }
        }

    }
    void UpdateHighScores(HighScoreData[] highScores)
    {
        for (int i = 0; i < highScoresPrefabs.Length; i++)
        {
            highScoresPrefabs[i].SetActive(false);
        }
        for(int i = 0; i < highScores.Length; i++)
        {
            highScoresPrefabs[i].SetActive(true);
            usernameTexts[i].text = $"{i + 1}. {highScores[i].username}";
            highScoreTexts[i].text = $"{highScores[i].totalScore}";
        }
    }

    [Serializable]
    public class HighScoreData
    {
        public string username;
        public float totalScore;
    }
    
}

