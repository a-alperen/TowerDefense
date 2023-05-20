using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance { get; private set; }

    [Header("Texts and InputFields")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI gameMoneyText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI mathQuestionText;
    [SerializeField] private TMP_InputField mathAnswerField;
    [SerializeField] private TextMeshProUGUI warningPanelText;

    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;        // Oyun bitince açılan panel
    [SerializeField] private GameObject pausePanel;           // Oyun duraklatma paneli
    [SerializeField] private GameObject optionsPanel;         // Ayarlar paneli(Ses ve müzik kontrol)
    [SerializeField] private GameObject questionPanel;        // Soru paneli(Türkçe, hayat bilgisi)
    [SerializeField] private GameObject mathQuestionPanel;    // Soru paneli(Matematik)
    [SerializeField] private GameObject lectureChoosePanel;   // Ders seçim paneli
    [SerializeField] private GameObject warningPanel;         // Uyarı paneli

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
    public void UpdateUIText(float gameMoney, float time)
    {
        gameMoneyText.text = gameMoney.ToString();
        timeText.text = time.ToString("F1");
    }

    public void UpdateUIText(float gameMoney)
    {
        gameMoneyText.text = gameMoney.ToString();
    }

    public void ShowGameOverPanel()
    {
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

    public void ShowQuestionPanel()
    {
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

    public void ShowWarningPanel(string text)
    {
        warningPanelText.text = text;
        warningPanel.GetComponent<Image>().color = Color.red;
        warningPanel.GetComponent<Animator>().Play("Warning");
    }
}
