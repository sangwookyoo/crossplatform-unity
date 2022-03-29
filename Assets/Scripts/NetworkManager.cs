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
            if (NetworkManager._instance == null)
            {
                NetworkManager._instance = new NetworkManager();
            }
            return NetworkManager._instance;
        }
    }

    void Awake()
    {
        NetworkManager._instance = this;
    }
}
