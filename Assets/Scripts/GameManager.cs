using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public bool isFirst = false;

    // Singleton
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

        UIManager.Instance.button.onClick.AddListener(() =>
        {
            GameObject.Instantiate(UIManager.Instance.userInfoObject, UIManager.Instance.userInfoContent);
        });

        UIManager.Instance.button01.onClick.AddListener(() =>
        {
            StartCoroutine(UIManager.Instance.ScreenCoroutine("Screen Test Num: " + num, 5f));
            num++;
        });

        UIManager.Instance.button02.onClick.AddListener(() =>
        {
            StartCoroutine(UIManager.Instance.SystemCoroutine("System Test Num: " + num, 3f));
            num++;
        });
    }

    void ChangeView()
    {
        UIManager.Instance.changeViewButton.onClick.AddListener(() =>
        {
            Vector3 firstPersonCameraOffset = PlayerManager.Instance.firstPersonCameraOffset;
            Vector3 thirdPersonCameraOffset = PlayerManager.Instance.thirdPersonCameraOffset;

            PlayerManager.Instance.mainCamera.transform.localPosition = (
                (PlayerManager.Instance.mainCamera.transform.localPosition == thirdPersonCameraOffset) ? firstPersonCameraOffset : thirdPersonCameraOffset
            );
        });
    }

    void Clock()
    {
        UIManager.Instance.clockText.text = DateTime.Now.ToString("tt h : mm : ss");
    }

    void ChangeSensitivity()
    {
        if ((Input.GetKeyDown(KeyCode.LeftBracket)) && (0f < PlayerManager.Instance.sensitivity))
        {
            PlayerManager.Instance.sensitivity -= 10f;
            StartCoroutine(UIManager.Instance.SystemCoroutine("마우스 감도: " + (int)PlayerManager.Instance.sensitivity / 10, 3f));
        }

        if ((Input.GetKeyDown(KeyCode.RightBracket)) && (PlayerManager.Instance.sensitivity < 1000f))
        {
            PlayerManager.Instance.sensitivity += 10f;
            StartCoroutine(UIManager.Instance.SystemCoroutine("마우스 감도: " + (int)PlayerManager.Instance.sensitivity / 10, 3f));
        }
    }
}
