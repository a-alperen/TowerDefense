using System.Linq;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UI;
public class RadioButtonManager : MonoBehaviour
{
    ToggleGroup toggleGroup;
    private GameManager gameManager;
    private UISystem uiManager;

    public string lecture;
    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UISystem>();
    }

    public void SubmitAnswer()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Time.timeScale = 1;
        uiManager.CloseQuestionPanel();

        if(toggle.name == "Option1")
        {
            Debug.Log("Dogru bildin!");
            gameManager.DecreaseEnemyHealth();
        }
        else
        {
            Debug.Log("Yanlis bildin!");
            gameManager.DecreasePlayerHealth();
        }
    }

}
