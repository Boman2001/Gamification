using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dtos;
using Enum;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HomeSceneUiController : MonoBehaviour
{
    [FormerlySerializedAs("testtext")] [SerializeField]
    public TMP_Text hearingImpairedButton;

    [FormerlySerializedAs("ChangeSubmissionButton")] [SerializeField]
    public Button changeSubmissionButton;
    
    [FormerlySerializedAs("LibraryButton")] [SerializeField]
    public Button libraryButton;
    
    [FormerlySerializedAs("VotingButton")] [SerializeField]
    public Button votingButton;
    // Start is called before the first frame update
    void Start()
    {
        hearingImpairedButton.text = DataStorageManager.Instance.PlayerType switch
        {
            PlayerType.Hearing => "the user is hearing impaired",
            PlayerType.Seight => "the user is seight impaired",
            PlayerType.Vistor => "the user is a visitor",
            PlayerType.Staff => "the user is staff",
            _ => hearingImpairedButton.text
        };

        changeSubmissionButton.onClick.AddListener( () => { });
        libraryButton.onClick.AddListener( () => { SceneManager.LoadScene("Library"); });
        votingButton.onClick.AddListener( () => { });
    }
}
