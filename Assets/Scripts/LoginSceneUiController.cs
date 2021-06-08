using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Config;
using Dtos;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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
        
        if (request.isNetworkError || request.isHttpError)
        {
            var responseText = request.downloadHandler.text;
            try
            {
                var response = JObject.Parse(responseText);
                var errorMessage = responseText;
            
                if (response["message"] != null)
                {
                    errorMessage = response["message"].ToString();
                }
                
                //StartCoroutine(ShowToast($"Error {errorMessage}", 1));
            }
            catch (Exception e)
            {
                //StartCoroutine(ShowToast($"Error {responseText}", 1));
            }
        }
        else
        {
            var response = JObject.Parse(request.downloadHandler.text);
            if (response["token"] != null)
            {
                PlayerPrefs.SetString("token", response["token"].ToString());
               //StartCoroutine(ShowToast($"Success!",1));
                GoToScene(sceneOnSuccess);
            }
            else
            {
                //StartCoroutine(ShowToast($"Error Token not found",1));
            }
        }
    }
    //
    // IEnumerator ShowToast(string text, int duration)
    // {
    //     toast.enabled = true;
    //     toast.text = text;
    //     float counter = 0;
    //     while (counter < duration)
    //     {
    //         counter += Time.deltaTime;
    //         yield return null;
    //     }
    //     
    //     toast.enabled = false;
    // }
    
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
        //StartCoroutine(ShowToast($" {scenePref}", 100000));
    }

}
