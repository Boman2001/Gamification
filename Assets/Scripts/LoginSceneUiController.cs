using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Config;
using Dtos;
using Enum;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoginSceneUiController : MonoBehaviour
{
    [FormerlySerializedAs("HearingImpairedButton")] [SerializeField]
    public Button hearingImpairedButton;
    
    [FormerlySerializedAs("SightImpairedButton")] [SerializeField]
    public Button sightImpairedButton;
    
    [FormerlySerializedAs("VisitorButton")] [SerializeField]
    public Button userButton;
    
    [FormerlySerializedAs("BackButton")] [SerializeField]
    public Button backButton;
    
    [FormerlySerializedAs("EmailInput")] [SerializeField]
    public TMP_InputField emailInput;
        
    [FormerlySerializedAs("PasswordInput")] [SerializeField]
    public TMP_InputField passwordInput;
    
    [FormerlySerializedAs("Toast")] [SerializeField]
    public TMP_Text toast;

    // Start is called before the first frame update
    void Start()
    {
        //Todo: If Token is set and valid automatically log in and continue, wss gwn een /me en als die faalt log uit
        hearingImpairedButton.onClick.AddListener( () => { Login(ScenePref.Hearing); });
        sightImpairedButton.onClick.AddListener( () => { Login(ScenePref.Seight); });
        userButton.onClick.AddListener( () => { Login(ScenePref.Vistor); });
        backButton.onClick.AddListener(() => {GoToScene(ScenePref.Account);});
    }

    void Login(ScenePref scenePref)
    {
        var loginDto = new LoginDto()
        { 
            Email = emailInput.text,
            Password = passwordInput.text
        };

        var obj = JsonUtility.ToJson(loginDto);
        StartCoroutine(Post(ServerConfig.API_URL + "/auth/login", obj, scenePref));
    }
    
    IEnumerator Post(string url, string bodyJsonString, ScenePref sceneOnSuccess)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.ConnectionError  || request.result == UnityWebRequest.Result.ProtocolError  )
        {
            var responseText = request.downloadHandler.text;
            try
            {
                var response = JObject.Parse(responseText);

                if (response["message"] != null)
                {
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        else
        {
            var response = JObject.Parse(request.downloadHandler.text);
            if (response["token"] != null)
            {
                DataStorageManager.Instance.RequestToken = response["token"].ToString();
                GoToScene(sceneOnSuccess);
            }
        }
    }
    
    enum ScenePref
    {
        Hearing,
        Seight,
        Vistor,
        Staff,
        Account
    }
    
    void GoToScene(ScenePref scenePref)
    {
        switch (scenePref)
        {
            case ScenePref.Account:
                SceneManager.LoadScene("Register");
                break;
            case ScenePref.Hearing:
                DataStorageManager.Instance.PlayerType = PlayerType.Hearing;
                SceneManager.LoadScene("Home");
                break;
            case ScenePref.Seight:
                DataStorageManager.Instance.PlayerType = PlayerType.Seight;
                SceneManager.LoadScene("Home");
                break;
            case ScenePref.Staff:
                DataStorageManager.Instance.PlayerType = PlayerType.Staff;
                SceneManager.LoadScene("Home");
                break;
            case ScenePref.Vistor:
                DataStorageManager.Instance.PlayerType = PlayerType.Vistor;
                SceneManager.LoadScene("Home");
                break;
        }
    }

}
