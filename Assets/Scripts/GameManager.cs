using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Singleton */
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (GameManager._instance == null)
            {
                GameManager._instance = new GameManager();
            }
            return GameManager._instance;
        }
    }

    void Awake()
    {
        GameManager._instance = this;
    }
}
