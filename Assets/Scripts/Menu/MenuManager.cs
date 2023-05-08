using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int gold;

    
    public GameObject marketPanel;

    public TMP_Text goldText;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitMarket()
    {
        marketPanel.SetActive(false);
    }

    public void EnterMarket()
    {
        marketPanel.SetActive(true);
    }

    private void Start()
    {
        goldText.text = gold.ToString();
    }

}
