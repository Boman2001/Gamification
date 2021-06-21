using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PerformanceSceneUiController : MonoBehaviour
{
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void navigateBack()
    {
        SceneManager.LoadScene("Library");
    }
}
