using TMPro;
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
    
    // Start is called before the first frame update
    void Start()
    {
        if (DataStorageManager.Instance.MusicSubmission.Length > 1)
        {
            changeSubmissionButton.GetComponentInChildren<TMP_Text>().text = "Inzending Aanpassen";
        }
        
        changeSubmissionButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("AvatarVisuallyIImpaired");
        });
        logoutButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Register");
        });
        libraryButton.onClick.AddListener( () => { SceneManager.LoadScene("Library"); });
        votingButton.onClick.AddListener( () => { });
    }
}
