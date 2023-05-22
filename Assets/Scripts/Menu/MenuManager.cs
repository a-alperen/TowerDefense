using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public GameObject marketPanel;

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

}
