using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LibrarySceneUiController : MonoBehaviour
{
    [FormerlySerializedAs("InventoryButton")] [SerializeField]
    public Button inventoryButton;
    
    [FormerlySerializedAs("ShopButton")] [SerializeField]
    public Button shopButton;
    
    [FormerlySerializedAs("BackButton")] [SerializeField]
    public Button backButton;
    
    [FormerlySerializedAs("PerformancesButton")] [SerializeField]
    public Button performanceButton;
    
    [FormerlySerializedAs("RankingsBUtton")] [SerializeField]
    public Button rankingButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryButton.onClick.AddListener(() => { SceneManager.LoadScene("Inventory"); });
        shopButton.onClick.AddListener( () => { SceneManager.LoadScene("Shop"); });
        backButton.onClick.AddListener( () => { SceneManager.LoadScene("Home"); });
        performanceButton.onClick.AddListener( () => { SceneManager.LoadScene("Performances"); });
        rankingButton.onClick.AddListener( () => { SceneManager.LoadScene("Ranking"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
