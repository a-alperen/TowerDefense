using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    DatabaseConnection connection;
    [Header("Warning Panel")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningPanelText;
    [Header("Question Send Fields")]
    [SerializeField] private TMP_InputField questionText;
    [SerializeField] private TMP_InputField optionAText;
    [SerializeField] private TMP_InputField optionBText;
    [SerializeField] private TMP_InputField optionCText;
    [SerializeField] private TMP_Dropdown answerDropdown;
    [SerializeField] private TMP_Dropdown lectureDropdown;
    [Header("Other Stuff")]
    [SerializeField] private GameObject questionSendButton;

    private bool canSendQuestion = false;

    private void Update()
    {
        if(!canSendQuestion)
        {
            if(MarketManager.Instance.data.totalScore >= 25000)
            {
                canSendQuestion = true;
                questionSendButton.SetActive(true);
            }
        }
    }

    private void Start()
    {
        connection = GetComponent<DatabaseConnection>();

    }
    public void StartGame()
    {
        StartCoroutine(LoadGame());

        IEnumerator LoadGame()
        {
            yield return connection.SetData(PlayerPrefs.GetString("username"), MarketManager.Instance.data);
            yield return SceneManager.LoadSceneAsync("Game");
        }
    }
    
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ExitGame()
    {
        StartCoroutine(Exit());

        IEnumerator Exit()
        {
            yield return connection.SetData(PlayerPrefs.GetString("username"), MarketManager.Instance.data);
            Application.Quit();
        }
    }
    public void SendQuestion()
    {
        if(questionText.text == "" || optionAText.text == "" || optionBText.text == "" || optionCText.text == "")
        {
            ShowWarningPanel("Alanları boş bırakmayın.", Color.red);
            return;
        }
        
        StartCoroutine(Send());
        
        IEnumerator Send()
        {
            yield return StartCoroutine(connection.SendQuestion(lectureDropdown.captionText.text.ToString(), questionText.text, optionAText.text, optionBText.text, optionCText.text, answerDropdown.captionText.text.ToString(), PlayerPrefs.GetString("username")));
            
            questionText.text = string.Empty;
            optionAText.text = string.Empty;
            optionBText.text = string.Empty;
            optionCText.text = string.Empty;
        }

        void ShowWarningPanel(string text, Color color)
        {
            warningPanelText.text = text;
            warningPanel.GetComponent<Image>().color = color;
            warningPanel.GetComponent<Animator>().Play("Warning");
        }
    }
}
