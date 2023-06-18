using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseConnection : MonoBehaviour
{
    
    /// <summary>
    /// Veritabanından gelen veriyi dataya kaydeder.
    /// </summary>
    /// <param name="data"></param>
    private void SetValue(string[] userData,Data data)
    {
        data.totalScore = float.Parse(userData[0]);          //totalScore
        data.totalGold = float.Parse(userData[1]);           //totalGold
        data.highScore = float.Parse(userData[2]);           //highScore
        data.highGold = float.Parse(userData[3]);            //highGold
        data.marketGold = float.Parse(userData[4]);          //marketGold
        
        int[] correctAnswers = Array.ConvertAll(userData[6].Split("/"),int.Parse);
        int[] wrongAnswers = Array.ConvertAll(userData[7].Split("/"), int.Parse);
        data.correctAnswers = correctAnswers.ToList();
        data.wrongAnswers = wrongAnswers.ToList();

        string[] tempUpgradeLevels = userData[5].Split(":");
        List<List<int>> upgradeLevels = new();
        for(int i = 0; i < tempUpgradeLevels.Length; i++)
        {
            int[] temp = Array.ConvertAll(tempUpgradeLevels[i].Split("/"),int.Parse);
            upgradeLevels.Add(temp.ToList());
        }
        
        string[] tempUpgradePowers = userData[8].Split(":");
        List<List<float>> upgradePowers = new();
        for (int i = 0; i < tempUpgradePowers.Length; i++)
        {
            float[] temp = Array.ConvertAll(tempUpgradePowers[i].Split("/"), float.Parse);
            upgradePowers.Add(temp.ToList());
        }
        data.upgradeLevels = upgradeLevels;
        data.upgradePowers = upgradePowers;

    }

    public IEnumerator GetUserData(string username, Data data)
    {
        WWWForm form = new();
        form.AddField("unity", "veriCekme");
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("https://localhost/TowerDefense/UserData.php", form))
        {
            www.certificateHandler = new CertificateWhore();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string temp = www.downloadHandler.text;
                string[] userData = temp.Split("-");
                SetValue(userData, data);
                Debug.Log("Veri getirildi.");
            }
        }

    }
    public IEnumerator SetData(string username, Data data, float second=3f)
    {
        while (true)
        {
            yield return StartCoroutine(SetUserData(username, data));
            yield return new WaitForSeconds(second);
            
        }
    }
    public IEnumerator SetData(string username, Data data)
    {
        yield return StartCoroutine(SetUserData(username, data));
    }
    private IEnumerator SetUserData(string username, Data data)
    {
        string upgradeLevels = "";
        string correctAnswers = "";
        string wrongAnswers = "";
        string upgradePowers = "";

        ConvertData();

        WWWForm form = new();
        form.AddField("unity", "veriKaydetme");
        form.AddField("username", username);
        form.AddField("totalScore", data.totalScore.ToString());
        form.AddField("totalGold", data.totalGold.ToString());
        form.AddField("highScore", data.highScore.ToString());
        form.AddField("highGold", data.highGold.ToString());
        form.AddField("marketGold", data.marketGold.ToString());
        form.AddField("upgradeLevels", upgradeLevels);
        form.AddField("correctAnswer", correctAnswers);
        form.AddField("wrongAnswer", wrongAnswers);
        form.AddField("upgradePower", upgradePowers);

        using (UnityWebRequest www = UnityWebRequest.Post("https://localhost/TowerDefense/UserData.php", form))
        {
            www.certificateHandler = new CertificateWhore();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Sorgu sonucu: " + www.downloadHandler.text);
            }
        }
        void ConvertData()
        {

            for (int i = 0; i < data.upgradeLevels.Count; i++)
            {
                for (int j = 0; j < data.upgradeLevels[i].Count; j++)
                {
                    if (j != 0) upgradeLevels += "/";
                    upgradeLevels += data.upgradeLevels[i][j].ToString("F0");
                }
                if(i != data.upgradeLevels.Count - 1) upgradeLevels += ":";
            }
            for (int i = 0; i < data.correctAnswers.Count; i++)
            {
                if (i != 0) correctAnswers += "/";
                correctAnswers += data.correctAnswers[i].ToString("F0");
            }
            for (int i = 0; i < data.wrongAnswers.Count; i++)
            {
                if (i != 0) wrongAnswers += "/";
                wrongAnswers += data.wrongAnswers[i].ToString("F0");
            }
            for (int i = 0; i < data.upgradePowers.Count; i++)
            {
                for (int j = 0; j < data.upgradePowers[i].Count; j++)
                {
                    if (j != 0) upgradePowers += "/";
                    upgradePowers += data.upgradePowers[i][j].ToString("F2");
                }
                if (i != data.upgradePowers.Count - 1) upgradePowers += ":";
            }
        }
    }
}
