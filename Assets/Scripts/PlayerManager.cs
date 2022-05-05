using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Object")]
    public PlayerDataScriptableObject playerDataScriptableObject;

    [Header("Player Settings")]
    public float sensitivity = 3f;
    public float moveSpeed = 8f;
    public float gravity = -9.8f;
    public float jumpPower = 2f;
    public int maxJumpCount = 2;

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
    public Vector3 firstPersonCameraOffset = new Vector3(0f, 1.8f, 0.3f);
    public Vector3 thirdPersonCameraOffset = new Vector3(0f, 2f, -3.5f);
    public bool renderCameraX, renderCameraY, renderCameraZ;

    [Header("Animator Settings")]
    public RuntimeAnimatorController runtimeAnimatorController;
    public Avatar avatar;

    [Header("Modules")]
    public EventSystem eventSystem;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Camera mainCamera;
    [HideInInspector]
    public Camera renderCamera;

    // Singleton
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerManager();
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
        LoadPlayer();
    }

    void LoadPlayer()
    {
        player = Instantiate(playerDataScriptableObject.playerObject[0]) as GameObject;
        player.gameObject.name = "Player";
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.AddComponent<PlayerController>();

        mainCamera = Instantiate(mainCameraObject) as Camera;
        mainCamera.transform.SetParent(player.transform);
        mainCamera.transform.localPosition = PlayerManager.Instance.thirdPersonCameraOffset;

        renderCamera = Instantiate(renderCameraObject) as Camera;

        Instantiate(eventSystem);
    }
}