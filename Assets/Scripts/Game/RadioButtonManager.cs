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

        }
        else
        {
            Debug.Log("Yanlis bildin!");
            GameManager.Instance.gameMoney -= 25;

        }
    }

}
