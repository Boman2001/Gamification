using System;
using System.Collections.Generic;
using Databases;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class UiInventory : MonoBehaviour
    {
        public GameObject slotPrefab;
        public GameObject topPrefab;
        public MusicDatabase database;
        public GameObject listPost;
        public IList<Song> selectedSongs;
        public AudioSource source;
        private bool isPlaying;
        
        private void Awake()
        {
            isPlaying = false;
            selectedSongs = new List<Song>();
            GameObject newObj; // Create GameObject instance

            for (int i = 0; i < database.songs.Count; i++)
            {
                // Create new instances of our prefab until we've created as many as we specified
                newObj = (GameObject)Instantiate(slotPrefab, transform);
                UiItem item = newObj.GetComponentInChildren<UiItem>();
                item.song = database.FindSongById(i);
                item.text.text = item.song.title;
                item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    TaskOnClick(item);
                });
                
              
            }
        }
        
        void TaskOnClick(UiItem song){    
            GameObject newObj = (GameObject)Instantiate(topPrefab, listPost.transform);
            
            UiItem item = newObj.GetComponent<UiItem>();
            
            GameObject playbutton = null;
            GameObject playbuttonText = null; 
            GameObject deletebutton = null;
            GameObject hearingTitle = null;
            GameObject deletebuttonText = null; 

            
            for (int i = 0; i < item.gameObject.transform.childCount; i++)
            {
                Transform child =  item.gameObject.transform.GetChild(i);
                if (child.tag == "HearingPlay")
                    playbutton = child.gameObject;
                if(child.tag == "HearingDelete")
                    deletebutton = child.gameObject;
                if(child.tag == "HearingSongTitle")
                    hearingTitle = child.gameObject;
            }
            
            for (int i = 0; i < playbutton.gameObject.transform.childCount; i++)
            {
                Transform child =  playbutton.gameObject.transform.GetChild(i);
                Debug.Log(child.tag);
                if (child.tag == "HearingPlayText")
                    playbuttonText = child.gameObject;
            }

            for (int i = 0; i < deletebutton.gameObject.transform.childCount; i++)
            {
                Transform child =  deletebutton.gameObject.transform.GetChild(i);
                Debug.Log(child.tag);
                if (child.tag == "HearingDeleteText")
                    deletebuttonText = child.gameObject;
            }

                TMP_Text deleteText = deletebuttonText.GetComponent<TMP_Text>();
                TMP_Text PlayText =  playbuttonText.GetComponent<TMP_Text>();
                deleteText.text = "Delete";
                PlayText.text = "Play";
                hearingTitle.GetComponent<TMP_Text>().text = song.song.title;
            
                Debug.Log(PlayText.text);
                Debug.Log(deleteText.text);
                
                selectedSongs.Add(song.song);

                deletebutton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Destroy(newObj);
                });
                
                playbutton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (isPlaying == false)
                    {
                        PlayText.text = "Stop";
                        source.clip = song.song.song;
                        source.Play();
                        isPlaying = true;
                    }
                    else
                    {
                        PlayText.text = "Play";
                        isPlaying = false;
                        source.Stop();
                    }
                });
        }
    }
}