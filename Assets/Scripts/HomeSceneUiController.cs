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
    
    
    [SerializeField]
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
        
        if (DataStorageManager.Instance.SubmissionSent)
        {
            matchText.text = "Inzending Verstuurd";
        }
    }

    public void SubmissionBehavior()
    {
        switch(DataStorageManager.Instance.PlayerType) {
            
            case PlayerType.Hearing:
                SceneManager.LoadScene(!MatchFound || DataStorageManager.Instance.SubmissionSent ? "SubmissionCreateCharacter" : "ConfirmSubmissionScene");
                return;

            case PlayerType.Seight:
                SceneManager.LoadScene(!MatchFound || DataStorageManager.Instance.SubmissionSent ? "AvatarVisuallyIImpaired" : "ConfirmSubmissionScene");
                return;  
        }
    }

    public void LogoutBehaviour()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Register");
    }
    public void NavigateToSettings()
    {
        SceneManager.LoadScene("Settings");

    }

    public void NavigateToLibrary()
    {
        SceneManager.LoadScene("Library");
    }

    IEnumerator CoRoutineSubmissionStub()
    {
        yield return new WaitForSeconds(5);
        changeSubmissionButton.GetComponentInChildren<TMP_Text>().text = "Inzending afmaken";
        matchText.text = "Match gevonden";
        MatchFound = true;
    }
    
    
}
