using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.Networking;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get;private set; }

    // Matematik panel text alanlari
    [Header("Texts")]
    public TMP_InputField answerField;

    private int number1, number2,result;
    public Question question;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        question = new Question();
        AskLecture();
        
    }
    
    /// <summary>
    /// Soru paneli ekrani gelir.
    /// </summary>
    public void AskQuestion()
    {
        GameManager.Instance.isPaused = true;

        StartCoroutine(Ask());

        IEnumerator Ask()
        {
            yield return StartCoroutine(GetQuestion(LectureManager.lecture));
            if (LectureManager.lecture == "Matematik")
            {
                UISystem.Instance.ShowMathQuestionPanel(AskMathQuestion());
            }
            else if (LectureManager.lecture == "Hayat Bilgisi")
            {
                UISystem.Instance.ShowQuestionPanel(question);
            }
            else if (LectureManager.lecture == "Türkçe")
            {
                UISystem.Instance.ShowQuestionPanel(question);
            }
        }
        IEnumerator GetQuestion(string lecture)
        {
            WWWForm form = new();

            form.AddField("Unity", "SoruGetir");
            form.AddField("Lecture", lecture);

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
                    string responseText = www.downloadHandler.text;
                    if (responseText.Contains("Soru bulunamadı."))
                    {
                        Debug.Log("Soru getirilemedi.");
                    }
                    else
                    {
                        question = JsonUtility.FromJson<Question>(responseText);
                        Debug.Log($"{lecture} sorusu getirildi.");
                    }
                    
                }
            }
        }
    }

    public void AskLecture()
    {
        GameManager.Instance.isPaused = true;
        UISystem.Instance.ShowLectureChoosePanel();
    }

    public string AskMathQuestion()
    {
        int operationChooser = UnityEngine.Random.Range(1, 5);
        char operation;
        string questionText = "";

        switch (operationChooser)
        {
            case 1: // Toplama
                operation = '+';
                number1 = UnityEngine.Random.Range(0,20);
                number2 = UnityEngine.Random.Range(0,20);
                result = number1 + number2;
                questionText = $"{number1} {operation} {number2} = ?";
                break;
            case 2: // Cikarma
                operation = '-';
                number1 = UnityEngine.Random.Range(0, 20);
                number2 = UnityEngine.Random.Range(0, 20);
                result = (number1 > number2) ? number1 - number2 : number2 - number1;
                questionText = $"{(number1 > number2 ? number1 : number2)} {operation} {(number1 > number2 ? number2 : number1)} = ?";
                break;
            case 3: // Carpma
                operation = 'x';
                number1 = UnityEngine.Random.Range(1, 11);
                number2 = UnityEngine.Random.Range(1, 11);
                result = number1 * number2;
                questionText = $"{number1} {operation} {number2} = ?";
                break;
            case 4: // Bolme
                operation = '/';
                while (true)
                {
                    number1 = UnityEngine.Random.Range(1, 21);
                    number2 = UnityEngine.Random.Range(1, 21);
                    if (number1 > number2 && number1 % number2 == 0) break;
                }
                result = number1 / number2;
                questionText = $"{number1} {operation} {number2} = ?";
                break;

            default:
                break;
        }
        
        return questionText;
    }

    public void CheckMathAnswer()
    {
        try
        {
            
            if (answerField.text == "")
            {
                throw new ArgumentNullException("Cevap alanı boş!");
            }
            else
            {
                int playerAnswer = Convert.ToInt32(answerField.text);

                Debug.Log(playerAnswer);
                if (playerAnswer == result)
                {
                    Debug.Log("Yühuuuuu!");
                    GameManager.Instance.gameMoney += 50;
                    GameManager.Instance.gold += UnityEngine.Random.Range(100,150);
                    GameManager.Instance.score += UnityEngine.Random.Range(100, 500);
                    GameManager.Instance.correctCount++;
                }
                else
                {
                    Debug.Log("AAAAAAAA!");
                    if(GameManager.Instance.gameMoney -25 >= 0) GameManager.Instance.gameMoney -= 25;
                    GameManager.Instance.score -= 100;
                    GameManager.Instance.wrongCount++;
                }
                UISystem.Instance.CloseMathQuestionPanel();
                GameManager.Instance.isPaused = false;
            }
        }
        catch (ArgumentNullException e)
        {
            //uiManager.ShowWarningPanel("Cevap alanı boş!");
            Debug.Log(e.Message);

        }
        
        
    }

    
}
