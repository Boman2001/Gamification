using System;

using Enum;

using UnityEngine;


namespace Singletons {
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

        public string RequestToken
        {
            get
            {
                return PlayerPrefs.GetString("token");
            }
            set
            {
                PlayerPrefs.SetString("token", value);
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
