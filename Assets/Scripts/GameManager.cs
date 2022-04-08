using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Modules")]
    UIManager UIManager;
    SoundManager SoundManager;

    [Header("Modules Object")]
    public GameObject UIManagerObj;
    public GameObject SoundManagerObj;

    /* Singleton */
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this);
        }

        UIManager = GetClone(UIManagerObj).GetComponent<UIManager>();
        SoundManager = GetClone(SoundManagerObj).GetComponent<SoundManager>();
    }
    
    GameObject GetClone(GameObject Obj) {
        return Instantiate(Obj) as GameObject;
    }
}
