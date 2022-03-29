using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /* Singleton */
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (UIManager._instance == null)
            {
                UIManager._instance = new UIManager();
            }
            return UIManager._instance;
        }
    }

    void Awake()
    {
        UIManager._instance = this;
    }
}
