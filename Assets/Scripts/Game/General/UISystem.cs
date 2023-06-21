using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance { get; private set; }

    [Header("Stat Panel Texts")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI statsCorrectText;
    [SerializeField] private TextMeshProUGUI statsWrongText;
    [SerializeField] private TextMeshProUGUI statsScoreText;
    [SerializeField] private TextMeshProUGUI statsGoldText;
    [Header("Game Over Panel Texts")]
    [SerializeField] private TextMeshProUGUI titleText;         
    [SerializeField] private TextMeshProUGUI scoreText;         
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI correctText;
    [SerializeField] private TextMeshProUGUI wrongText;
    [Header("Other Texts")]
    [SerializeField] private TextMeshProUGUI gameMoneyText;
    [SerializeField] private TextMeshProUGUI mathQuestionText;
    [SerializeField] private TMP_InputField mathAnswerField;
    [Header("Question Panel Stuff")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Toggle optionA;
    [SerializeField] private Toggle optionB;
    [SerializeField] private Toggle optionC;

    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;        // Oyun bitince açılan panel
    [SerializeField] private GameObject pausePanel;           // Oyun duraklatma paneli
    [SerializeField] private GameObject optionsPanel;         // Ayarlar paneli(Ses ve müzik kontrol)
    [SerializeField] private GameObject questionPanel;        // Soru paneli(Türkçe, hayat bilgisi)
    [SerializeField] private GameObject mathQuestionPanel;    // Soru paneli(Matematik)
    [SerializeField] private GameObject lectureChoosePanel;   // Ders seçim paneli
    [SerializeField] private GameObject skillPanel;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (int.TryParse(mathAnswerField.text, out _))
        {
            mathAnswerField.image.color = Color.white;
        }
        else
        {
            mathAnswerField.image.color = Color.red;
        }
    }
    public void UpdateUIText(float gameMoney, float time,int correctCount,int wrongCount,int score,int gold)
    {
        gameMoneyText.text = gameMoney.ToString();
        timeText.text = time.ToString("F1");
        statsCorrectText.text = $"Doğru: {correctCount}";
        statsWrongText.text = $"Yanlış: {wrongCount}";
        statsScoreText.text = $"Skor: {score}";
        statsGoldText.text = $"Altın: {gold}";
    }

    public void UpdateUIText(float gameMoney)
    {
        gameMoneyText.text = gameMoney.ToString();
    }

    public void ShowGameOverPanel(string title,int score,int gold,int correctCount,int wrongCount)
    {
        titleText.text = title;
        scoreText.text = $"Skorun: {score}";
        goldText.text = $"Altının: {gold}";
        correctText.text = $"Doğru Cevabın: {correctCount}";
        wrongText.text = $"Yanlış Cevabın: {wrongCount}";
        gameOverPanel.SetActive(true);
    }

    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ShowMathQuestionPanel(string text)
    {
        mathQuestionPanel.SetActive(true);
        mathQuestionText.text = text;
    }

    public void CloseMathQuestionPanel()
    {
        mathQuestionPanel.SetActive(false);
    }

    public void ShowQuestionPanel(Question question)
    {
        questionText.text = question.question_text;
        optionA.GetComponentInChildren<Text>().text = question.optionA_text;
        optionB.GetComponentInChildren<Text>().text = question.optionB_text;
        optionC.GetComponentInChildren<Text>().text = question.optionC_text;

        questionPanel.SetActive(true);
    }
    public void CloseQuestionPanel()
    {
        questionPanel.SetActive(false);
    }

    public void ShowLectureChoosePanel()
    {
        lectureChoosePanel.SetActive(true);
    }
    public void CloseLectureChoosePanel()
    {
        lectureChoosePanel.SetActive(false);
    }

    bool anim = true;
    public void SkillPanelAnim()
    {   
        if(anim) skillPanel.GetComponent<Animator>().Play("SkillPanelOpen");
        else skillPanel.GetComponent<Animator>().Play("SkillPanelClose");
        anim = !anim;
    }
}
