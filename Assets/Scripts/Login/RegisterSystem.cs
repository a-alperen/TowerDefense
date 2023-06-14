using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterSystem : MonoBehaviour
{

    [Header("Register UI Elements")]
    [Space(5)]
    public TMP_InputField registerUsername;
    public TMP_InputField registerPassword;
    public TMP_InputField registerrePassword;

    private string warningText;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }


    public void RegisterGame()
    {
        if (registerPassword.text == "" || registerrePassword.text == "" || registerUsername.text == "")
        {
            warningText = "Alanları boş bırakmayın!";
            uiManager.ShowWarningPanel(warningText, Color.red);
        }
        else if (registerUsername.text.Length < 4)
        {
            warningText = "Kullanıcı adı 4 karakterden kısa olamaz!";
            uiManager.ShowWarningPanel(warningText, Color.red);
        }
        else if (registerPassword.text.Length < 8)
        {
            warningText = "Şifre uzunluğu 8 karakterden kısa olamaz!";
            uiManager.ShowWarningPanel(warningText, Color.red);
        }
        else if (registerPassword.text != registerrePassword.text)
        {
            warningText = "Girdiğiniz şifreler eşleşmiyor!";
            uiManager.ShowWarningPanel(warningText, Color.red);
        }
        else
        {
            if (registerPassword.text == registerrePassword.text && registerPassword.text != "")
            {
                //Veri tabanı bağlantısı

                //username = registerUsername.text;
                //password = registerPassword.text;


                
                Debug.Log("Veri tabanına bağlanıldı.");
                StartCoroutine(Register());

                ClearUI();
                
            }
        }

    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "kayitOlma");
        form.AddField("username", registerUsername.text);
        form.AddField("password", registerPassword.text);


        using (UnityWebRequest www = UnityWebRequest.Post("https://192.168.1.37/TowerDefense/UserRegister.php", form))
        {
            www.certificateHandler = new CertificateWhore();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Sorgu sonucu:" + www.downloadHandler.text);
                uiManager.ShowWarningPanel(www.downloadHandler.text, Color.red);
            }
        }
    }

    private void ClearUI()
    {
        registerUsername.text = "";
        registerPassword.text = "";
        registerrePassword.text = "";
    }
}
