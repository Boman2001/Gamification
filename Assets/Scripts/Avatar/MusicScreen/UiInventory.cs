using System;
using System.Collections.Generic;
using Databases;
using Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class UiInventory : MonoBehaviour
    {
        public GameObject slotPrefab;
        public MusicDatabase database;
        public GameObject listPost;
        private void Awake()
        {
            
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
            UiItem post = Instantiate(song, listPost.transform);
        }
    }
}