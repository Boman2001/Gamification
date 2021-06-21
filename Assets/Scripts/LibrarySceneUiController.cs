using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LibrarySceneUiController : MonoBehaviour
{


    // Start is called before the first frame update
    // Update is called once per frame
    public void navigateToInventory()
    {
        SceneManager.LoadScene("Inventory");
    }
    
    public void navigateToShop()
    {
        SceneManager.LoadScene("Shop");
    }
    
    public void navigateToHome()
    {
        SceneManager.LoadScene("Home");
    }
    
    public void navigateToPreformances()
    {
        SceneManager.LoadScene("Performances");
    }
    
    public void navigateToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }
    
}
