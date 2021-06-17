using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Databases;
using Domain;
using Dtos;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Avatar.MusicScreen
{
    public class MusicSceneUiController : MonoBehaviour
    {
        [FormerlySerializedAs("Avatar top view prefab")] public GameObject avatarHoldingContent;
        [FormerlySerializedAs("Avatar bottom view prefab")] public GameObject avatarMusicLibraryContent;
        
        public GameObject slotPrefab;
        public GameObject topPrefab;
        public MusicDatabase database;
        public AudioSource source;
        public Slider volumeSlider;
        private List<TMP_Text> _playTextList;
        private List<UiItem> _uiItems;
        private List<Song> _selectedSongs;
        
        private void Start()
        {
            volumeSlider.value = DataStorageManager.Instance.Volume;
            _playTextList = new List<TMP_Text>();
            _uiItems = new List<UiItem>();
            _selectedSongs = new List<Song>();

            for (var i = 0; i < database.songs.Count; i++)
            {
                var newObj = Instantiate(slotPrefab, avatarMusicLibraryContent.transform);
                var item = newObj.GetComponentInChildren<UiItem>();
                item.song = database.FindSongById(i);
                item.text.text = item.song.title;
                item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    TaskOnClick(item);
                });
                _uiItems.Add(item);
            }

            if (DataStorageManager.Instance.MusicSubmission.Length <= 1) return;
            Deserialize(DataStorageManager.Instance.MusicSubmission);
        }

        public void Submit()
        {
            DataStorageManager.Instance.MusicSubmission = _selectedSongs.Count > 0 ? Serialize() : null;
            if (DataStorageManager.Instance.SubmissionSent)
            {
                SendSubmission();
            }   
            SceneManager.LoadScene("Home");
        }

        public void Back()
        {
             SceneManager.LoadScene("Home");
        }
        
        private string Serialize()
        {
            var musicListDto = new MusicListDto {SongDtos = new List<SongDto>()};
            
            foreach (var songDto in _selectedSongs.Select(song => new SongDto()
            {
                id = song.id.ToString(),
                title = song.title
            }))
            {
                musicListDto.SongDtos.Add(songDto);
            }
            
            return JsonUtility.ToJson(musicListDto);
        }

        private void Deserialize(string json)
        {
            var account = JsonUtility.FromJson<MusicListDto>(json);
            foreach (var item in account.SongDtos.Select(variable => _uiItems.FirstOrDefault(x => x.song.id.ToString() == variable.id)).Where(item => item != null))
            {
                TaskOnClick(item);
            }
        }

        private void TaskOnClick(UiItem song){    
            var newObj = Instantiate(topPrefab, avatarHoldingContent.transform);
            
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
                return;
            }

            var deleteText = deletebuttonText.GetComponent<TMP_Text>();
            var playText = playbuttonText.GetComponent<TMP_Text>();
            deleteText.text = "Delete";
            playText.text = "Play";
            hearingTitle.GetComponent<TMP_Text>().text = song.song.title;
            _playTextList.Add(playText);
            _selectedSongs.Add(song.song);

            deletebutton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (playText.text == "Stop")
                {
                    source.Stop();
                }
                _selectedSongs.Remove(song.song);
                Destroy(newObj);
            });

            playbutton.GetComponent<Button>().onClick.AddListener(() =>
            {
                StartStop(playText, song);
            });
        }
        void SendSubmission()
        {
            if (DataStorageManager.Instance.MusicSubmission.Length > 1)
            {
                StartCoroutine(Post("/tournaments", DataStorageManager.Instance.MusicSubmission)); 
                DataStorageManager.Instance.SubmissionSent = true; 
                SceneManager.LoadScene("Home");
            }
        }
        IEnumerator Post(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(url, "POST");
            var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler =  new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json-patch+json");
            yield return request.SendWebRequest();

        }
        private void OnEnable()
        {
            volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider.value); });
        }

        private void ChangeVolume(float sliderValue)
        {
            source.volume = sliderValue;
        }

        private void OnDisable()
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
        }
        private void StartStop(TMP_Text button, UiItem song)
        {
            source.Stop();

            if (button.text == "Stop")
            {
                foreach (var text in _playTextList)
                {
                    text.text = "Play";
                }
            }
            else
            {
                foreach (var text in _playTextList)
                {
                    text.text = "Play";
                }
                button.text = "Stop";
                source.clip = song.song.song;
                source.Play();
            } 
        }
    }
}