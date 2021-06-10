using System.Collections;
using System.Collections.Generic;
using Enum;
using Singletons;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HomeSceneUiController : MonoBehaviour
{

    [FormerlySerializedAs("ChangeSubmissionButton")] [SerializeField]
    public Button changeSubmissionButton;
    
    [FormerlySerializedAs("LibraryButton")] [SerializeField]
    public Button libraryButton;
    
    [FormerlySerializedAs("VotingButton")] [SerializeField]
    public Button votingButton;
    
    [FormerlySerializedAs("LogoutButton")] [SerializeField]
    public Button logoutButton;
    
    [FormerlySerializedAs("SettingsButton")] [SerializeField]
    public Button settingsButton;
    
    [FormerlySerializedAs("matchText")] [SerializeField]
    public TMP_Text matchText;

    private bool MatchFound;
    // Start is called before the first frame update
    void Start()
    {
        MatchFound = false;
      
        
        
        if (DataStorageManager.Instance.MusicSubmission.Length > 1)
        {
            changeSubmissionButton.GetComponentInChildren<TMP_Text>().text = "Inzending Aanpassen";
            StartCoroutine(CoRoutineSubmissionStub());
        }
        
        changeSubmissionButton.onClick.AddListener(() =>
        {
            if (DataStorageManager.Instance.PlayerType == PlayerType.Hearing)
            {
                if (!MatchFound)
                {
                    SceneManager.LoadScene("SubmissionCreateCharacter");
                }
                else
                {
                    SceneManager.LoadScene("ConfirmSubmissionScene");
                }
            }
        
            if (DataStorageManager.Instance.PlayerType == PlayerType.Seight)
            {
                if (!MatchFound)
                {
                    SceneManager.LoadScene("AvatarVisuallyIImpaired");
                }
                else
                {
                    SceneManager.LoadScene("ConfirmSubmissionScene");
                }
            }
        });
        
        settingsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Settings");
        });
        
        logoutButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Register");
        });
        libraryButton.onClick.AddListener( () => { SceneManager.LoadScene("Library"); });
        votingButton.onClick.AddListener( () => { });
    }

    IEnumerator CoRoutineSubmissionStub()
    {
        yield return new WaitForSeconds(5);
        changeSubmissionButton.GetComponentInChildren<TMP_Text>().text = "Inzending afmaken";
        matchText.text = "Match gevonden";
        MatchFound = true;
    }
}
