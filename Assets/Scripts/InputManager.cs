using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 look;
    [HideInInspector] public bool jump;

    private PlayerInpuActions _playerInputActions;
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
        _playerInputActions = new PlayerInpuActions();
    }

    void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Player.Move.started += OnMove;
        _playerInputActions.Player.Move.performed += OnMove;
        _playerInputActions.Player.Move.canceled += OnMove;

        _playerInputActions.Player.Look.started += OnLook;
        _playerInputActions.Player.Look.performed += OnLook;
        _playerInputActions.Player.Look.canceled += OnLook;

        _playerInputActions.Player.Jump.started += OnJump;
        _playerInputActions.Player.Jump.performed += OnJump;
        _playerInputActions.Player.Jump.canceled += OnJump;
    }

    void OnDisable()
    {
        _playerInputActions.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        jump = _playerInputActions.Player.Jump.triggered;
    }
}
