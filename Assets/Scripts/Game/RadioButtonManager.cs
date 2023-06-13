using System.Linq;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UI;
public class RadioButtonManager : MonoBehaviour
{
    ToggleGroup toggleGroup;

    public string lecture;
    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void SubmitAnswer()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        UISystem.Instance.CloseQuestionPanel();
        GameManager.Instance.isPaused = false;
        if(toggle.name == "Option1")
        {
            Debug.Log("Dogru bildin!");
            GameManager.Instance.gameMoney += 50;
            GameManager.Instance.gold += UnityEngine.Random.Range(100, 150);
            GameManager.Instance.score += UnityEngine.Random.Range(100, 500);
            GameManager.Instance.correctCount += 1;

        }
        else
        {
            Debug.Log("Yanlis bildin!");
            if (GameManager.Instance.gameMoney - 25 >= 0) GameManager.Instance.gameMoney -= 25;
            GameManager.Instance.score -= 100;
            GameManager.Instance.wrongCount += 1;

        }
    }

}
