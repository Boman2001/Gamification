using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PopupController : MonoBehaviour
{
    #region Singleton

    public static PopupController Instance;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(Container);
        } 
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            Destroy(Container);
        }
    }

    #endregion
    
    public TMP_Text PopupMessageText;

    public Image Container;
    // Start is called before the first frame update
    void Start()
    {
        Container.enabled = false;
        PopupMessageText.enabled = false;
    }

    public enum MessageType
    {
        Error,
        Information,
        Success
    }

    public IEnumerator ShowToast(string text, int duration, MessageType type )
    {
        Container.enabled = true;
        PopupMessageText.enabled = true;

        switch (type)
        {
            case MessageType.Error:
                Container.color = Color.red;
                break;
                
            case MessageType.Information:
                Container.color = Color.blue;
                break;
            
            case MessageType.Success:
                Container.color = Color.green;
                break;
        }
        
        PopupMessageText.text = text;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        
        Container.enabled = false;
        PopupMessageText.enabled = false;
    }
}
