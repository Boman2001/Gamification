using System;
using System.Collections;
using System.Collections.Generic;
using Enum;
using UnityEngine;

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
            switch (PlayerPrefs.GetInt("PlayerType"))
            {
                case 0:
                    return PlayerType.Hearing;
                break;
                
                case 1:
                    return PlayerType.Seight;
                    break;

                case 2:
                    return PlayerType.Staff;
                    break;

                case 3:
                    return PlayerType.Vistor;
                    break;
                
                default:
                    return PlayerType.Vistor;
                    break;
            }

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
