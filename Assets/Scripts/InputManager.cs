using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private FloatingJoystick _floatingJoystick;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 look;
    [HideInInspector] public bool jump;

    private PlayerInpuActions _playerInputActions;
    private InputAction _moveAction;

    // Singleton
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InputManager();
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

        if (_playerInputActions == null)
        {
            _playerInputActions = new PlayerInpuActions();
        }
    }

    void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Player.Move.performed += OnMove;
        _playerInputActions.Player.Move.canceled += OnMove;

        _playerInputActions.Player.Look.performed += OnLook;
        _playerInputActions.Player.Look.canceled += OnLook;
    }

    void OnDisable()
    {
        _playerInputActions.Disable();
    }

    void Start() {
        GetFloatingJoystick();
    }

    void Update()
    {
        #if UNITY_ANDROID || UNITY_IOS
            move.x = _floatingJoystick.Horizontal;
            move.y = _floatingJoystick.Vertical;
        #endif

        #if UNITY_EDITOR || UNITY_STANDALONE
            jump = _playerInputActions.Player.Jump.triggered;
        #endif
    }

    void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    void GetFloatingJoystick()
    {
        if (UIManager.Instance.uiController == null) return;
        _floatingJoystick = UIManager.Instance.uiController.GetComponent<FloatingJoystick>();
    }
}
