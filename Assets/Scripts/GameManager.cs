using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public float sensitivity = 1f;
    public float moveSpeed = 8f;
    public float gravity = -9.8f;
    public float jumpPower = 2f;
    public int maxJumpCount = 1;

    [Header("CharacterController Settings")]
    public float slopeLimit = 45f;
    public float stepOffset = 0.3f;
    public float skinWidth = 0.08f;
    public float minMoveDistance = 0.001f;
    public float radius = 0.23f;
    public float height = 1.8f;
    public Vector3 center = new Vector3(0f, 0.9f, 0f);

    [Header("Camera Object")]
    public Camera mainCameraObject;
    public Camera renderCameraObject;

    [Header("Camera Settings")]
    public bool isFirst = false;
    public bool renderCameraX = true;
    public bool renderCameraY = false;
    public bool renderCameraZ = true;
    public Vector3 firstPersonCameraOffset = new Vector3(0f, 1.8f, 0.3f);
    public Vector3 thirdPersonCameraOffset = new Vector3(0f, 2f, -3.5f);

    [Header("Animator Settings")]
    public RuntimeAnimatorController runtimeAnimatorController;
    public Avatar avatar;

    [HideInInspector] public GameObject player;
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public Camera renderCamera;

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

        mainCamera = Instantiate(mainCameraObject) as Camera;
        renderCamera = Instantiate(renderCameraObject) as Camera;
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
            Instantiate(UIManager.Instance.userInfoObject, UIManager.Instance.userInfoContent);
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
            mainCamera.transform.localPosition = (
                (mainCamera.transform.localPosition == thirdPersonCameraOffset) ? firstPersonCameraOffset : thirdPersonCameraOffset
            );
        });
    }

    void Clock()
    {
        UIManager.Instance.clockText.text = DateTime.Now.ToString("tt h : mm : ss");
    }

    void ChangeSensitivity()
    {
        if ((Input.GetKeyDown(KeyCode.LeftBracket)) && (0f < sensitivity))
        {
            sensitivity -= 1f;
            StartCoroutine(UIManager.Instance.SystemCoroutine("Mouse sensitivity: " + (int)sensitivity, 3f));
        }

        if ((Input.GetKeyDown(KeyCode.RightBracket)) && (sensitivity < 1000f))
        {
            sensitivity += 1f;
            StartCoroutine(UIManager.Instance.SystemCoroutine("Mouse sensitivity: " + (int)sensitivity, 3f));
        }
    }
}
