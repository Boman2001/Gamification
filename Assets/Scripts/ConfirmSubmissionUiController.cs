using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enum;
using Singletons;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ConfirmSubmissionUiController : MonoBehaviour
{
    [FormerlySerializedAs("SubmissionButton")] [SerializeField]
    public Button submissionButton;
    // Start is called before the first frame update
    void Start()
    {
        submissionButton.onClick.AddListener(() =>
        {
            SendSubmission();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SendSubmission()
    {
        if (DataStorageManager.Instance.PlayerType == PlayerType.Seight)
        {
            if (DataStorageManager.Instance.MusicSubmission.Length > 1)
            {
                StartCoroutine(Post("/tournaments", DataStorageManager.Instance.MusicSubmission));
                DataStorageManager.Instance.SubmissionSent = true;
                SceneManager.LoadScene("Home");
            }
        }

    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json-patch+json");
        yield return request.SendWebRequest();

    }
}
