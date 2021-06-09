using System.Collections.Generic;
using System.Linq;
using Databases;
using Domain;
using Dtos;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class UiInventory : MonoBehaviour
    {
        [FormerlySerializedAs("SubmitButton")] public Button submitButton;
        [FormerlySerializedAs("BackButton")] public Button backButton;
        
        public GameObject slotPrefab;
        public GameObject topPrefab;
        public MusicDatabase database;
        public GameObject listPost;
        public List<Song> selectedSongs;
        public AudioSource source;
        private bool _isPlaying;
        public List<UiItem> uiItems;
        private void Awake()
        {
            _isPlaying = false;
            uiItems = new List<UiItem>();
            selectedSongs = new List<Song>();
            
            submitButton.onClick.AddListener(() =>
            {
                DataStorageManager.Instance.MusicSubmission = selectedSongs.Count > 0 ? Serialize() : null;
                SceneManager.LoadScene("Home");
            }); 
            backButton.onClick.AddListener( () => { SceneManager.LoadScene("Home"); });
            
            GameObject newObj;

            for (var i = 0; i < database.songs.Count; i++)
            {
                newObj = Instantiate(slotPrefab, transform);
                var item = newObj.GetComponentInChildren<UiItem>();
                item.song = database.FindSongById(i);
                item.text.text = item.song.title;
                item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    TaskOnClick(item);
                });
                uiItems.Add(item);
            }

            if (DataStorageManager.Instance.MusicSubmission.Length <= 1) return;
            
            Deserialize(DataStorageManager.Instance.MusicSubmission);
        }

        private string Serialize()
        {
            var musicListDto = new MusicListDto {SongDtos = new List<SongDto>()};
            
            foreach (var songDto in selectedSongs.Select(song => new SongDto()
            {
                id = song.id.ToString(),
                title = song.title
            }))
            {
                musicListDto.SongDtos.Add(songDto);
            }
            
            return  JsonConvert.SerializeObject(musicListDto, Formatting.Indented);
        }

        private void Deserialize(string json)
        {
            var account = JsonConvert.DeserializeObject<MusicListDto>(json);
            foreach (var item in account.SongDtos.Select(variable => uiItems.FirstOrDefault(x => x.song.id.ToString() == variable.id)).Where(item => item != null))
            {
                TaskOnClick(item);
            }
        }

        private void TaskOnClick(UiItem song){    
            var newObj = Instantiate(topPrefab, listPost.transform);
            
            var item = newObj.GetComponent<UiItem>();
            
            GameObject playbutton = null;
            GameObject playbuttonText = null; 
            GameObject deletebutton = null;
            GameObject hearingTitle = null;
            GameObject deletebuttonText = null; 

            
            for (var i = 0; i < item.gameObject.transform.childCount; i++)
            {
                var child =  item.gameObject.transform.GetChild(i);
                if (child.CompareTag("HearingPlay"))
                    playbutton = child.gameObject;
                if(child.CompareTag("HearingDelete"))
                    deletebutton = child.gameObject;
                if(child.CompareTag("HearingSongTitle"))
                    hearingTitle = child.gameObject;
            }

            if (playbutton == null || deletebutton == null)
            {
                StartCoroutine(PopupController.Instance.ShowToast("Something went wrong", 100, PopupController.MessageType.Error));
                return;
            }
            
            for (var i = 0; i < playbutton.gameObject.transform.childCount; i++)
            {
                var child = playbutton.gameObject.transform.GetChild(i);
                if (child.CompareTag("HearingPlayText"))
                    playbuttonText = child.gameObject;
            }

            for (var i = 0; i < deletebutton.gameObject.transform.childCount; i++)
            {
                var child = deletebutton.gameObject.transform.GetChild(i);
                if (child.CompareTag("HearingDeleteText"))
                    deletebuttonText = child.gameObject;
            }

            if (deletebuttonText == null || playbuttonText == null || hearingTitle == null)
            {
                StartCoroutine(PopupController.Instance.ShowToast("Something went wrong", 100, PopupController.MessageType.Error));
                return;
            }
            
            
            var deleteText = deletebuttonText.GetComponent<TMP_Text>();
            var playText = playbuttonText.GetComponent<TMP_Text>();
            deleteText.text = "Delete";
            playText.text = "Play";
            hearingTitle.GetComponent<TMP_Text>().text = song.song.title;

            selectedSongs.Add(song.song);

            deletebutton.GetComponent<Button>().onClick.AddListener(() =>
            {
                selectedSongs.Remove(song.song);
                Destroy(newObj);
            });

            playbutton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (_isPlaying == false)
                {
                    playText.text = "Stop";
                    source.clip = song.song.song;
                    source.Play();
                    _isPlaying = true;
                }
                else
                {
                    playText.text = "Play";
                    _isPlaying = false;
                    source.Stop();
                }
            });
        }
    }
}