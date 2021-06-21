using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class ConfirmSubmissionSceneUIManager : MonoBehaviour {

    public void Confirm() {

        SceneManager.LoadScene("Home");
    }
}