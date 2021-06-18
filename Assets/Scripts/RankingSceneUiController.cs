using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RankingSceneUiController : MonoBehaviour
{

    public void navigateBack()
    {
        SceneManager.LoadScene("Library");
    }
}
