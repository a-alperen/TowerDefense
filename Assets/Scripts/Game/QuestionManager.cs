using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using static System.Net.Mime.MediaTypeNames;

public class QuestionManager : MonoBehaviour
{
    
    // Matematik panel text alanlari
    [Header("Texts")]
    public TMP_InputField answerField;

    private GameManager gameManager;
    private UISystem uiManager;

    private int number1, number2,result;
    
    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UISystem>();
        Time.timeScale = 0;
        AskLecture();
        
    }
    private void Update()
    {

    }

    
    /// <summary>
    /// Soru paneli ekrani gelir.
    /// </summary>
    public void AskQuestion()
    {
        Time.timeScale = 0;
        if (LectureManager.lecture == "matematik")
        {
            uiManager.ShowMathQuestionPanel(AskMathQuestion());
            //Time.timeScale = 0;
        }
        else if (LectureManager.lecture == "hayatbilgisi")
        {
            uiManager.ShowQuestionPanel();
            //Time.timeScale = 0;
        }
        else if (LectureManager.lecture == "turkce")
        {
            uiManager.ShowQuestionPanel();
            //Time.timeScale = 0;
        }

    }

    public void AskLecture()
    {
        uiManager.ShowLectureChoosePanel();
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
                    gameManager.DecreaseEnemyHealth();
                }
                else
                {
                    gameManager.DecreasePlayerHealth();
                }
                uiManager.CloseMathQuestionPanel();
                Time.timeScale = 1.0f;
            }
        }
        catch (ArgumentNullException e)
        {
            //uiManager.ShowWarningPanel("Cevap alanı boş!");
            Debug.Log(e.Message);

        }
        
        
    }

    
}
