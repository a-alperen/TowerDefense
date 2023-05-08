using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [Space(5)]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject warningPanel;


    [Header("Texts")]
    [Space(5)]
    public TextMeshProUGUI warningPanelText;

    public void ShowWarningPanel(string text, Color color)
    {
        warningPanelText.text = text;
        warningPanel.GetComponent<Image>().color = color;
        warningPanel.GetComponent<Animator>().Play("Warning");
    }
    public void ShowRegisterPanel()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ShowLoginPanel()
    {
        registerPanel.SetActive(false );
        loginPanel.SetActive(true );
    }

    
}
