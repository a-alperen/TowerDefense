using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LectureManager : MonoBehaviour
{
    ToggleGroup toggleGroup;
    private GameManager gameManager;
    private UISystem uiManager;

    public static string lecture;
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("GameManager").GetComponent<UISystem>();
    }
    public void ChooseLecture()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        uiManager.CloseLectureChoosePanel();
        gameManager.ResumeGame();

        if (toggle.name == "Option1")
        {
            lecture = "hayatbilgisi";
        }
        else if (toggle.name == "Option2")
        {
            lecture = "turkce";
        }
        else if (toggle.name == "Option3")
        {
            lecture = "matematik";
        }
    }
}
