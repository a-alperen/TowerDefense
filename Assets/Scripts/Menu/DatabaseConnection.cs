using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DatabaseConnection : MonoBehaviour
{
    [Header("Warning Panel")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningPanelText;

    #region Veri Kaydetme ve Veri Çekme
    /// <summary>
    /// Veritabanından gelen veriyi dataya kaydeder.
    /// </summary>
    /// <param name="data"></param>
    private void SetValueToData(UserData userData, Data data)
    {
        data.totalScore = userData.totalScore;          //totalScore
        data.totalGold = userData.totalGold;            //totalGold
        data.highScore = userData.highScore;            //highScore
        data.highGold = userData.highGold;              //highGold
        data.marketGold = userData.marketGold;          //marketGold

        data.correctAnswers = Array.ConvertAll(userData.correct_answers.Split("/"), int.Parse).ToList();
        data.wrongAnswers = Array.ConvertAll(userData.wrong_answers.Split("/"), int.Parse).ToList();

        data.upgradeLevels = TranslateInt(userData.upgrade_levels);
        data.upgradePowers = TranslateFloat(userData.upgrade_powers);

        // Veritabanından gelen upgradelevels string'ini float listesine çevirir.
        List<List<float>> TranslateFloat(string data)
        {
            List<List<float>> list = new();
            string[] tempString = data.Split(":");

            for (int i = 0; i < tempString.Length; i++)
            {
                float[] temp = Array.ConvertAll(tempString[i].Split("/"), float.Parse);
                list.Add(temp.ToList());
            }

            return list;
        }
        // Veritabanından gelen upgradepowers string'ini int listesine çevirir.
        List<List<int>> TranslateInt(string data)
        {
            List<List<int>> list = new();
            string[] tempString = data.Split(":");

            for (int i = 0; i < tempString.Length; i++)
            {
                int[] temp = Array.ConvertAll(tempString[i].Split("/"), int.Parse);
                list.Add(temp.ToList());
            }

            return list;
        }
    }

    /// <summary>
    /// Veri tabanından kullanıcı verisini getirir.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerator GetUserData(string username, Data data)
    {
        WWWForm form = new();
        form.AddField("Unity", "VeriCekme");
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
                string responseText = www.downloadHandler.text;
                UserData userData = JsonUtility.FromJson<UserData>(responseText);
                SetValueToData(userData, data);
                Debug.Log("Veri getirildi.");
            }
        }

    }
    /// <summary>
    /// Kullanıcı verisini veri tabanına kaydeder.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="data"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    public IEnumerator SetData(string username, Data data, float second = 3f)
    {
        while (true)
        {
            yield return StartCoroutine(SetUserData(username, data));
            yield return new WaitForSeconds(second);

        }
    }
    /// <summary>
    /// Kullanıcı verisini veri tabanına kaydeder.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IEnumerator SetData(string username, Data data)
    {
        yield return StartCoroutine(SetUserData(username, data));
    }
    private IEnumerator SetUserData(string username, Data data)
    {
        string correctAnswers = ConvertAnswerCount(data.correctAnswers);
        string wrongAnswers = ConvertAnswerCount(data.wrongAnswers);
        string upgradeLevels = ConvertUpgradeLevels(data.upgradeLevels);
        string upgradePowers = ConvertUpgradePowers(data.upgradePowers);

        WWWForm form = new();
        form.AddField("Unity", "VeriKaydetme");
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
        // Doğru ve yanlış sayısının tutulduğu liste string'e çevirir.
        string ConvertAnswerCount(List<int> answers)
        {
            string text = string.Empty;

            for (int i = 0; i < answers.Count; i++)
            {
                if (i != 0) text += "/";
                text += answers[i].ToString("F0");
            }
            return text;
        }
        // Upgrade levels listesini string'e çevirir.
        string ConvertUpgradeLevels(List<List<int>> list)
        {
            string text = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j != 0) text += "/";
                    text += list[i][j].ToString("F0");
                }
                if (i != list.Count - 1) text += ":";
            }
            return text;
        }
        // Upgrade powers listesini string'e çevirir.
        string ConvertUpgradePowers(List<List<float>> list)
        {
            string text = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j != 0) text += "/";
                    text += list[i][j].ToString("F2");
                }
                if (i != list.Count - 1) text += ":";
            }
            return text;
        }
    }
    #endregion

    #region Soru Kaydetme
    
    public IEnumerator SendQuestion(string lecture,string questionText,string optionA,string optionB,string optionC,string correctAnswer,string username)
    {
        string answer = Answer(correctAnswer);
        WWWForm form = new();
        form.AddField("Unity", "SoruGonder");
        form.AddField("Lecture", lecture);
        form.AddField("QuestionText",questionText);
        form.AddField("OptionA", optionA);
        form.AddField("OptionB", optionB);
        form.AddField("OptionC", optionC);
        form.AddField("CorrectAnswer", answer);
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("https://localhost/TowerDefense/Question.php", form))
        {
            www.certificateHandler = new CertificateWhore();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text.Contains("Soru eklenemedi."))
                {
                    ShowWarningPanel(www.downloadHandler.text, Color.red);
                }
                else
                {
                    ShowWarningPanel(www.downloadHandler.text, Color.green);
                }
                
                
            }
        }
        string Answer(string correctAnswer)
        {
            return correctAnswer switch
            {
                "A Şıkkı" => "A",
                "B Şıkkı" => "B",
                "C Şıkkı" => "C",
                _ => ""
            };
        }
        void ShowWarningPanel(string text, Color color)
        {
            warningPanelText.text = text;
            warningPanel.GetComponent<Image>().color = color;
            warningPanel.GetComponent<Animator>().Play("Warning");
        }
    }
    #endregion

}
[Serializable]
public class UserData
{
    public float totalScore;
    public float totalGold;
    public float highScore;
    public float highGold;
    public float marketGold;
    public string upgrade_levels;
    public string correct_answers;
    public string wrong_answers;
    public string upgrade_powers;
}

[Serializable]
public class Question
{
    public string question_text;
    public string optionA_text;
    public string optionB_text;
    public string optionC_text;
    public string correct_answer;
}
