using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    [Header("Player Controller")]
    public PlayerController _playerController;

    [Header("Player Object")]
    public PlayerDataScriptableObject playerDataScriptableObject;

    [Header("Player Settings")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpPower = 2;
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
    public Camera MainCamera;

    [Header("Camera Settings")]
    // TODO: zoom etc...

    [Header("Animator Settings")]
    public RuntimeAnimatorController runtimeAnimatorController;
    public Avatar avatar;

    [Header("Modules")]
    public PlayerInpuActions playerInputActions;
    public EventSystem eventSystem;

    [Header("Singleton")]
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
        GameObject player = Instantiate(playerDataScriptableObject.playerObject[0]) as GameObject;
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        _playerController = player.AddComponent<PlayerController>();
    }
}