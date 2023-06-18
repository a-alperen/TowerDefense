using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    DatabaseConnection connection;
    private void Start()
    {
        connection = GetComponent<DatabaseConnection>();
    }
    public void StartGame()
    {
        StartCoroutine(LoadGame());

        IEnumerator LoadGame()
        {
            yield return connection.SetData(PlayerPrefs.GetString("username"), MarketManager.Instance.data);
            yield return SceneManager.LoadSceneAsync("Game");
        }
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
        StartCoroutine(Exit());

        IEnumerator Exit()
        {
            yield return connection.SetData(PlayerPrefs.GetString("username"), MarketManager.Instance.data);
            Application.Quit();
        }
    }
    
}
