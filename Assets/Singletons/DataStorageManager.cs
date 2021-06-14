using System;
using Enum;
using UnityEngine;

namespace Singletons
{
    public class DataStorageManager : MonoBehaviour
    {

        #region Singleton

        public static DataStorageManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        #endregion

        public bool SubmissionSent
        {
            get
            {
                return PlayerPrefs.GetInt("SubmissionSent") switch
                {
                    0 => false,
                    1 => true,
                    _ => false
                };
            }
            set
            {
                switch (value)
                {
                    case true:
                        PlayerPrefs.SetInt("SubmissionSent", 1);
                        break;
                    case false:
                        PlayerPrefs.SetInt("SubmissionSent", 0);
                        break;
                }
            }
        }

        public string RequestToken
        {
            get { return PlayerPrefs.GetString("token"); }
            set { PlayerPrefs.SetString("token", value); }
        }

        public string MusicSubmission
        {
            get { return PlayerPrefs.GetString("MusicSubmission"); }
            set { PlayerPrefs.SetString("MusicSubmission", value); }
        }

        public float Volume
        {
            get
            {
                return PlayerPrefs.GetFloat("Volume");
            
            }
            set
            {
                PlayerPrefs.SetFloat("Volume", value);
            }
        }

        public PlayerType PlayerType
        {
            get
            {
                return PlayerPrefs.GetInt("PlayerType") switch
                {
                    0 => PlayerType.Hearing,
                    1 => PlayerType.Seight,
                    2 => PlayerType.Staff,
                    3 => PlayerType.Vistor,
                    _ => PlayerType.Vistor
                };
            }
            set
            {
                switch (value)
                {
                    case PlayerType.Hearing:
                        PlayerPrefs.SetInt("PlayerType", 0 );
                        break;
                
                    case PlayerType.Seight:
                        PlayerPrefs.SetInt("PlayerType", 1 );
                        break;

                    case PlayerType.Staff:
                        PlayerPrefs.SetInt("PlayerType", 2 );
                        break;

                    case PlayerType.Vistor:
                        PlayerPrefs.SetInt("PlayerType", 3 );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }
    }
}
