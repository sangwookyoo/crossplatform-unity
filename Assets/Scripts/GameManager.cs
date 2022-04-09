using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public bool isFirst = false;

    [Header("Modules")]
    public UIManager UIManager;
    public SoundManager SoundManager;

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

    GameObject GetClone(GameObject Obj)
    {
        return Instantiate(Obj) as GameObject;
    }

    void Start()
    {
        DebugButton();
        ChangeView();
    }

    void Update()
    {
        Clock();
        ChangeSensitivity();
    }

    void DebugButton()
    {
        int num = 1;

        UIManager.button.onClick.AddListener(() =>
        {
            GameObject.Instantiate(UIManager.userInfoObject, UIManager.userInfoContent);
        });

        UIManager.button01.onClick.AddListener(() =>
        {
            StartCoroutine(UIManager.ScreenCoroutine("Screen Test Num: " + num, 5f));
            num++;
        });

        UIManager.button02.onClick.AddListener(() =>
        {
            StartCoroutine(UIManager.SystemCoroutine("System Test Num: " + num, 3f));
            num++;
        });
    }

    void ChangeView()
    {
        UIManager.changeViewButton.onClick.AddListener(() =>
        {
            // PlayerManager.Instance.mainCamera.transform.localPosition = PlayerManager.Instance.firstPersonCameraOffset;
        });
    }

    void Clock()
    {
        UIManager.clockText.text = DateTime.Now.ToString("tt h : mm : ss");
    }

    void ChangeSensitivity()
    {
        if (PlayerManager.Instance.sensitivity > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                PlayerManager.Instance.sensitivity -= 10f;
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                PlayerManager.Instance.sensitivity += 10f;
            }
        }
    }
}
