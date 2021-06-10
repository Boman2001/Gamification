using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Config;
using Dtos;
using Enum;
using Newtonsoft.Json.Linq;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RegisterSceneUiController : MonoBehaviour
{
    [FormerlySerializedAs("HearingImpairedButton")] [SerializeField]
    public Button hearingImpairedButton;
    
    [FormerlySerializedAs("SightImpairedButton")] [SerializeField]
    public Button sightImpairedButton;
    
    [FormerlySerializedAs("VisitorButton")] [SerializeField]
    public Button userButton;
    
    [FormerlySerializedAs("AccountButton")] [SerializeField]
    public Button accountButton;
    
    [FormerlySerializedAs("EmailInput")] [SerializeField]
    public TMP_InputField emailInput;
    
     [FormerlySerializedAs("NameInput")] [SerializeField]
     public TMP_InputField nameInput;
        
    [FormerlySerializedAs("PasswordInput")] [SerializeField]
    public TMP_InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Todo: If Token is set and valid automatically log in and continue, wss gwn een /me en als die faalt log uit
        hearingImpairedButton.onClick.AddListener( () => { Register(ScenePref.Hearing); });
        sightImpairedButton.onClick.AddListener( () => { Register(ScenePref.Seight); });
        userButton.onClick.AddListener( () => { Register(ScenePref.Vistor); });
        accountButton.onClick.AddListener(() => { GoToScene(ScenePref.Account); });
    }

    void Register(ScenePref scenePref)
    {
        var registerDto = new RegisterDto
        {
            Email = emailInput.text, Password = passwordInput.text, Username = nameInput.text
        };

        var obj = JsonUtility.ToJson(registerDto);
        StartCoroutine(Post(ServerConfig.API_URL + "/auth/register", obj, scenePref));
    }
    
    IEnumerator Post(string url, string bodyJsonString, ScenePref sceneOnSuccess)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.isNetworkError || request.isHttpError || request.error != null)
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
                
                StartCoroutine(PopupController.Instance.ShowToast($"Error {errorMessage}", 1, PopupController.MessageType.Error));
            }
            catch (Exception e)
            {
                StartCoroutine(PopupController.Instance.ShowToast($"Error {request.error}", 10, PopupController.MessageType.Information));
            }
        }
        else
        {
            var response = JObject.Parse(request.downloadHandler.text);
            if (response["token"] != null)
            {
                DataStorageManager.Instance.RequestToken = response["token"].ToString();
                StartCoroutine(PopupController.Instance.ShowToast($"Success!",1, PopupController.MessageType.Success));
                GoToScene(sceneOnSuccess);
            }
            else
            {
                StartCoroutine(PopupController.Instance.ShowToast($"Error Token not found",1, PopupController.MessageType.Error));
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
        StartCoroutine(PopupController.Instance.ShowToast($" {scenePref}", 10, PopupController.MessageType.Success));
        
        switch (scenePref)
        {
            case ScenePref.Account:
                SceneManager.LoadScene("Login");
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
