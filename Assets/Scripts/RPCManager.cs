using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCManager : MonoBehaviour
{
    /* Singleton */
    private static RPCManager _instance;

    public static RPCManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RPCManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
}
