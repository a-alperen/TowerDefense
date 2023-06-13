using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void StartGame()
    {
        SaveSystem.SaveData(MarketManager.Instance.data, "PlayerStats");
        SceneManager.LoadScene("Game");
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
