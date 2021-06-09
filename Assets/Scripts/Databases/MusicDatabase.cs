using System.Collections.Generic;
using System.Linq;
using Domain;
using UnityEngine;

namespace Databases
{
    public class MusicDatabase : MonoBehaviour
    {
        public List<Song> songs = new List<Song>();

        public List<Song> _Songs
        {
            get => songs;
            set => songs = value;
        }

        public Song FindSongById(int id)
        {
            return songs.FirstOrDefault(x => x.id == id);
        }
    }
}