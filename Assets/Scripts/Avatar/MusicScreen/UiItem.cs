using System;
using Domain;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class UiItem : MonoBehaviour
    {
        public Song song;
        public TMP_Text text;

        [CanBeNull]
        public Button DeleteButton;
        
        [CanBeNull]
        public Button PlayButton;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        
        
        
    }
}