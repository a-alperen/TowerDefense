using TMPro;
using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    [Header("Total Panel Texts")]
    [SerializeField] TextMeshProUGUI allScoreText;
    [SerializeField] TextMeshProUGUI allGoldText;
    [Header("Score Panel Texts")]
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI highGoldText;
    [Header("Turkish Panel Texts")]
    [SerializeField] TextMeshProUGUI turkishCorrectText;
    [SerializeField] TextMeshProUGUI turkishWrongText;
    [Header("HB Panel Texts")]
    [SerializeField] TextMeshProUGUI hbCorrectText;
    [SerializeField] TextMeshProUGUI hbWrongText;
    [Header("Math Panel Texts")]
    [SerializeField] TextMeshProUGUI mathCorrectText;
    [SerializeField] TextMeshProUGUI mathWrongText;
    Data data;
    private void Start()
    {
        data = SaveSystem.SaveExists("PlayerStats")
               ? SaveSystem.LoadData<Data>("PlayerStats")
               : new Data();
        InvokeRepeating(nameof(UpdateTexts), 0, 5f);
    }
    public void UpdateTexts()
    {
        allScoreText.text = $"{data.totalScore:0}";
        allGoldText.text = $"{data.totalGold:0}";
        highScoreText.text = $"{data.highScore:0}";
        highGoldText.text = $"{data.highGold:0}";
        turkishCorrectText.text = $"{data.correctAnswers[0]:0}";
        turkishWrongText.text = $"{data.wrongAnswers[0]:0}";
        hbCorrectText.text = $"{data.correctAnswers[1]:0}";
        hbWrongText.text = $"{data.wrongAnswers[1]:0}";
        mathCorrectText.text = $"{data.correctAnswers[2]:0}";
        mathWrongText.text = $"{data.wrongAnswers[2]:0}";

    }
}
