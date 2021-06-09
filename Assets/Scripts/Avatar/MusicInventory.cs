using System.Collections.Generic;
using Avatar.MusicScreen;
using Databases;
using Domain;
using UnityEngine;

namespace Avatar
{
    public class MusicInventory : MonoBehaviour
    {
        public List<Song> AvatarSongs = new List<Song>();
        public MusicDatabase musicDatabase;
        public UiInventory inventory;

        public void GiveMusic(int id)
        {
            Song songToAdd = musicDatabase.FindSongById(id);
            AvatarSongs.Add(songToAdd);
        }

        public void RemoveMusic(int id)
        {
            AvatarSongs.Remove(AvatarSongs.Find(x => x.id == id));
        }

    }
}