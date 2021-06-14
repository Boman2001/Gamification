using UnityEngine;

namespace Domain
{
    [System.Serializable]
    public class Song
    {
        public int id;
        public string title;
        public AudioClip song;

        public Song(int id, string title, AudioClip song)
        {
            this.id = id;
            this.title = title;
            this.song = song;
        }
    }
}