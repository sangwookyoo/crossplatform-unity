using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* Singleton */
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (AudioManager._instance == null)
            {
                AudioManager._instance = new AudioManager();
            }
            return AudioManager._instance;
        }
    }

    void Awake()
    {
        AudioManager._instance = this;
    }
}