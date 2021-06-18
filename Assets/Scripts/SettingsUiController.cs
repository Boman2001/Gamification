using System.Collections;
using System.Collections.Generic;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsUiController : MonoBehaviour
{
    public Slider volumeSlider;
    
    [FormerlySerializedAs("VolumeTestButton")] [SerializeField]
    public Button VolumeTest;

    
    [FormerlySerializedAs("AudioSource")] [SerializeField]
    public AudioSource audioSource;

    private bool isPLaying;

    void Start()
    {
        isPLaying = false;
        volumeSlider.value = DataStorageManager.Instance.Volume;
        volumeSlider.value = DataStorageManager.Instance.Volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavigateBack()
    {
        SceneManager.LoadScene("Home");
    }

    public void TestAudio()
    {
        if (isPLaying == false)
        {
            audioSource.Play();
            VolumeTest.GetComponentInChildren<TMP_Text>().text = "Stop";
            isPLaying = true;
        }
        else
        {
            audioSource.Stop();
            VolumeTest.GetComponentInChildren<TMP_Text>().text = "Test";
            isPLaying = false;
        }
    }
    
    void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    void changeVolume(float sliderValue)
    {
        audioSource.volume = sliderValue;
        DataStorageManager.Instance.Volume = sliderValue;
    }

    void OnDisable()
    {
        volumeSlider.onValueChanged.RemoveAllListeners();
    }
}
