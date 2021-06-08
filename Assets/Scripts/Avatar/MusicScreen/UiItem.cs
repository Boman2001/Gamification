using System;
using Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class UiItem : MonoBehaviour
    {
        public Song song;
        public TMP_Text text;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        
        
        
    }
}