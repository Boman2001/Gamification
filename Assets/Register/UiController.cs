
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dtos;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] public Button RegisterButton;
    [SerializeField] public TMP_InputField NameInput;
    [SerializeField] public TMP_InputField PasswordInput;
    [SerializeField] public TMP_Text Toast;

    // Start is called before the first frame update
    void Start()
    {
        RegisterButton.onClick.AddListener(Register);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Register()
    {
        var registerDto = new RegisterDto()
        { 
            Email = NameInput.text,
            Password = PasswordInput.text
        };
        var obj = JsonUtility.ToJson(registerDto).ToString();
        StartCoroutine(Post("http://localhost:5000/api/v1/auth/register",obj));
    }
    
    
    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.isNetworkError || request.isHttpError)
        {
            StartCoroutine(showToast("Error",1));
        }
        else
        {
            string responseText = request.downloadHandler.text;
            StartCoroutine(showToast("Success",1));
        }
    }

    IEnumerator showToast(string text,
        int duration)
    {
        Toast.enabled = true;
        Toast.text = text;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        
        Toast.enabled = false;
        yield break;
    }
}
