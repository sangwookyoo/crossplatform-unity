using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float sensitivity;

    // PlayerManager Property
    private float _moveSpeed;
    private float _gravity;
    private float _jumpPower;
    private int _maxJumpCount;
    private Camera _mainCamera;

    // PlayerController Property
    private float _bodyAngle;
    private float _cameraAngle;
    private float _yVelocity = 0;
    private int _currentJumpCount = 0;
    private PlayerState _state = PlayerState.IDLE;

    // PlayerController Input
    private PlayerInpuActions _playerInputActions;
    private InputAction _move;
    private InputAction _fire;
    private Vector2 _playerInput;

    // PlayerController Components
    private CharacterController _characterController;
    private Animator _animator;

    enum PlayerState
    {
        IDLE = 0,
        MOVE = 1,
        JUMP = 2,  // TODO
        SIZE
    }

    void OnEnable()
    {
        _move = _playerInputActions.Player.Move;
        _move.Enable();

        _fire = _playerInputActions.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }

    void OnDisable()
    {
        _move.Disable();
        _fire.Disable();
    }

    void Awake()
    {
        _playerInputActions = new PlayerInpuActions();
    }

    void Start()
    {
        GetPlayerSettings();
    }

    void Update()
    {
        Move();
        RotateBody();
        RotateCamera();
        SetPlayerFSM();
        SetPlayerState();
        SetPlayerSettings();
    }

    void LateUpdate()
    {
        SetBlendTree();
        // TODO: Camera
    }

    void SetPlayerSettings()
    {
        // Player Settings
        sensitivity = PlayerManager.Instance.sensitivity;
    }

    void GetPlayerSettings()
    {
        // Player Settings
        _moveSpeed = PlayerManager.Instance.moveSpeed;
        _gravity = PlayerManager.Instance.gravity;
        _jumpPower = PlayerManager.Instance.jumpPower;
        _maxJumpCount = PlayerManager.Instance.maxJumpCount;

        // Camera Settings
        _mainCamera = PlayerManager.Instance.mainCamera;

        // CharacterController Settings
        _characterController = this.gameObject.AddComponent<CharacterController>();
        _characterController.slopeLimit = PlayerManager.Instance.slopeLimit;
        _characterController.stepOffset = PlayerManager.Instance.stepOffset;
        _characterController.skinWidth = PlayerManager.Instance.skinWidth;
        _characterController.minMoveDistance = PlayerManager.Instance.minMoveDistance;
        _characterController.radius = PlayerManager.Instance.radius;
        _characterController.height = PlayerManager.Instance.height;
        _characterController.center = PlayerManager.Instance.center;

        // Animator Settings
        _animator = this.gameObject.AddComponent<Animator>();
        _animator.runtimeAnimatorController = PlayerManager.Instance.runtimeAnimatorController;
        _animator.avatar = PlayerManager.Instance.avatar;
    }

    void Move()
    {
        _playerInput = _move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(_playerInput.x, 0, _playerInput.y).normalized;
        moveDir = _mainCamera.transform.TransformDirection(moveDir);

        if (_characterController.collisionFlags == CollisionFlags.Below)
        {
            _yVelocity = 0;
            _currentJumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpCount++;
            _yVelocity = _jumpPower;
        }

        _yVelocity += _gravity * Time.deltaTime;
        moveDir.y = _yVelocity;
        _characterController.Move(moveDir * Time.deltaTime * _moveSpeed);
    }

    void RotateBody()
    {
        _bodyAngle += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        this.transform.localEulerAngles = new Vector3(0, _bodyAngle, 0);
    }

    void RotateCamera()
    {
        _cameraAngle += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, -60, 60);
        _mainCamera.transform.localEulerAngles = new Vector3(-_cameraAngle, 0, 0);
    }

    void SetPlayerFSM()
    {
        switch (_state)
        {
            case PlayerState.IDLE:
                _animator.SetInteger("State", 0);
                break;

            case PlayerState.MOVE:
                _animator.SetInteger("State", 1);
                break;

            case PlayerState.JUMP:
                _animator.SetInteger("State", 2);
                break;
        }
    }

    void SetPlayerState()
    {
        if (_playerInput == Vector2.zero)
        {
            _state = PlayerState.IDLE;
        }
        else
        {
            _state = PlayerState.MOVE;
        }
    }

    void SetBlendTree()
    {
        _playerInput = _move.ReadValue<Vector2>();

        _animator.SetFloat("xDir", _playerInput.x);
        _animator.SetFloat("zDir", _playerInput.y);
    }

    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}