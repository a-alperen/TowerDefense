using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LectureManager : MonoBehaviour
{
    ToggleGroup toggleGroup;
    
    public static string lecture;
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }
    public void ChooseLecture()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        UISystem.Instance.CloseLectureChoosePanel();
        GameManager.Instance.ResumeGame();

        if (toggle.name == "Option1")
        {
            lecture = "Hayat Bilgisi";
        }
        else if (toggle.name == "Option2")
        {
            lecture = "Türkçe";
        }
        else if (toggle.name == "Option3")
        {
            lecture = "Matematik";
        }
    }
}
