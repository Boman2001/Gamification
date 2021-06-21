using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VotingUIController : MonoBehaviour
{
    [FormerlySerializedAs("ContinueButton")] [SerializeField]
    public Button continueButton;
    
    [FormerlySerializedAs("CodeInput")] [SerializeField]
    public TMP_InputField CodeInput;


    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(() => { ValidateCode();});
    }


    void ValidateCode()
    {
        if (CodeInput.text.Equals("5678"))
        {
            SceneManager.LoadScene("Voting");
        }
    }

}
