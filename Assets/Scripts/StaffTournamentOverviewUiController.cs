using System.Collections;
using System.Collections.Generic;
using System.Text;
using Config;
using Dtos.Tournaments;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StaffTournamentOverviewUiController : MonoBehaviour
{
    [FormerlySerializedAs("TournamentInput")] [SerializeField]
    public TMP_InputField tournamentInput;
    
    [FormerlySerializedAs("TournamentSubmit")] [SerializeField]
    public Button tournamentSubmit;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Get(ServerConfig.API_URL + "/Tournaments"));
        tournamentSubmit.onClick.AddListener(() => { PostTournament(); });
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PostTournament()
    {
        var createTournamentDto = new CreateTournamentDto()
        {
            Name = tournamentInput.text
        };

        var obj = JsonUtility.ToJson(createTournamentDto);
        StartCoroutine(Post(ServerConfig.API_URL + "/Tournaments", obj));
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.SetRequestHeader("Authorization", "Bearer " + DataStorageManager.Instance.RequestToken);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }
    
    IEnumerator Get(string url)
    {
        var request = new UnityWebRequest(url, "GET");
        request.SetRequestHeader("Authorization", "Bearer " + DataStorageManager.Instance.RequestToken);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        var response = JsonUtility.FromJson<TournamentGetDto>(request.downloadHandler.text);
        Debug.Log("here");
    }
}
