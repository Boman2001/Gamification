using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySceneUiController : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [FormerlySerializedAs("BackButton")] [SerializeField]
    public Button backButton;
    void Start()
    {
        backButton.onClick.AddListener( () => { SceneManager.LoadScene("Library"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
