using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    /* Singleton */
    private static NetworkManager _instance;

    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetworkManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }
}
