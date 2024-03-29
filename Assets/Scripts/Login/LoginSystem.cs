﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{

    [Header("Login UI Elements")]
    [Space(5)]
    public TMP_InputField loginUsername;
    public TMP_InputField loginPassword;

    private string warningText;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void LoginGame()
    {
        
        if (loginUsername.text == "" || loginPassword.text == "")
        {
            warningText = "Alanları boş bırakmayın!";
            uiManager.ShowWarningPanel(warningText, Color.red);
        }
        else
        {
            //Veri tabanı bağlantısı

            StartCoroutine(Login(loginUsername.text,loginPassword.text));
            ClearUI();
            
        }

    }

    /// <summary>
    /// Veri tabanına bağlanır.
    /// </summary>
    /// <returns></returns>
    IEnumerator Login(string username, string password)
    {
        WWWForm form = new();
        form.AddField("Unity", "GirisYapma");
        form.AddField("username", username);
        form.AddField("password", password);
        string Username = username;
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TowerDefense/UserRegister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                uiManager.ShowWarningPanel("Veri tabanına bağlanılamadı.", Color.red);
            }
            else
            {
                
                if(www.downloadHandler.text.Contains("Giriş başarılı."))
                {
                    uiManager.ShowWarningPanel(www.downloadHandler.text, Color.green);
                    PlayerPrefs.SetString("username", Username);
                    StartCoroutine(LoadScene());
                    
                }
                else
                {
                    uiManager.ShowWarningPanel(www.downloadHandler.text, Color.red);
                }

                
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("Menu");
    }
    private void ClearUI()
    {
        loginUsername.text = "";
        loginPassword.text = "";
    }

    
}
