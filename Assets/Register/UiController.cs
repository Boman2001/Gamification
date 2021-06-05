using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [FormerlySerializedAs("SlechtHorendButton")] [SerializeField]
    public Button hearingImpairedButton;
    
    [FormerlySerializedAs("SlechtZiendButton")] [SerializeField]
    public Button sightImpairedButton;
    
    [FormerlySerializedAs("GebruikerButton")] [SerializeField]
    public Button userButton;
    
    [FormerlySerializedAs("AccountButton")] [SerializeField]
    public Button accountButton;
    
    [FormerlySerializedAs("EmailInput")] [SerializeField]
    public TMP_InputField emailInput;
    
     [FormerlySerializedAs("NameInput")] [SerializeField]
     public TMP_InputField nameInput;
        
    [FormerlySerializedAs("PasswordInput")] [SerializeField]
    public TMP_InputField passwordInput;
    
    [FormerlySerializedAs("Toast")] [SerializeField]
    public TMP_Text toast;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Todo: If Token is set and valid automatically log in and continue, wss gwn een /me en als die faalt log uit
        hearingImpairedButton.onClick.AddListener(async () =>
        {
            Register(ScenePref.Hearing);
        });
        
        sightImpairedButton.onClick.AddListener(async () =>
        {
           Register(ScenePref.Seight);
        });
        
        userButton.onClick.AddListener(async () =>
        {
            Register(ScenePref.Vistor);
        });
        
        accountButton.onClick.AddListener(() =>
        { 
            GoToScene(ScenePref.Account);
        });
    }

    void Register(ScenePref scenePref)
    {
        var registerDto = new RegisterDto()
        { 
            Email = emailInput.text,
            Password = passwordInput.text,
            Username = nameInput.text
        };
        
        var obj = JsonUtility.ToJson(registerDto).ToString();
        StartCoroutine(Post("http://localhost:5000/api/v1/auth/register", obj, scenePref));
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
                
                StartCoroutine(ShowToast($"Error {errorMessage}", 1));
            }
            catch (Exception e)
            {
                StartCoroutine(ShowToast($"Error {responseText}", 1));
            }
        }
        else
        {
            var response = JObject.Parse(request.downloadHandler.text);
            if (response["token"] != null)
            {
                PlayerPrefs.SetString("token", response["token"].ToString());
                StartCoroutine(ShowToast($"Success!",1));
                GoToScene(sceneOnSuccess);
            }
            else
            {
                StartCoroutine(ShowToast($"Error Token not found",1));
            }
        }
    }

    IEnumerator ShowToast(string text, int duration)
    {
        toast.enabled = true;
        toast.text = text;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        
        toast.enabled = false;
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
        StartCoroutine(ShowToast($" {scenePref}", 100000));
    }

}
