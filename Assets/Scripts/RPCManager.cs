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
            if (RPCManager._instance == null)
            {
                RPCManager._instance = new RPCManager();
            }
            return RPCManager._instance;
        }
    }

    void Awake()
    {
        RPCManager._instance = this;
    }
}
