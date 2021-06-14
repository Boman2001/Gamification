using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VotingSelect : MonoBehaviour
{
    [FormerlySerializedAs("ButtonTeam1")] [SerializeField]
    public Button buttonTeam1;

    [FormerlySerializedAs("ButtonTeam2")] [SerializeField]
    public Button buttonTeam2;
    [FormerlySerializedAs("ButtonTeam3")] [SerializeField]
    public Button buttonTeam3;
    [FormerlySerializedAs("ButtonTeam4")] [SerializeField]
    public Button buttonTeam4;
    [FormerlySerializedAs("BackButton")] [SerializeField]
    public Button buttonBack;
    
    void Start()
    {
        buttonTeam1.onClick.AddListener(() => {MockVote();});
        buttonTeam2.onClick.AddListener(() => {MockVote();});
        buttonTeam3.onClick.AddListener(() => {MockVote();});
        buttonTeam4.onClick.AddListener(() => {MockVote();});
        buttonBack.onClick.AddListener(() => {  SceneManager.LoadScene("VotingCode");});
    }

    void MockVote()
    {
        StartCoroutine(PopupController.Instance.ShowToast($"Success!",1, PopupController.MessageType.Success));
        Invoke(nameof(navigate), 1.0f);
        
    }

    void navigate()
    {
        SceneManager.LoadScene("Home");
    }
}
