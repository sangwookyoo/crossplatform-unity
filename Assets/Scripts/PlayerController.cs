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
    private Camera _renderCamera;
    private bool _renderCameraX, _renderCameraY, _renderCameraZ;

    // PlayerController Property
    private float _bodyAngle;
    private float _cameraAngle;
    private float _yVelocity = 0;
    private int _currentJumpCount = 0;
    private PlayerState _state = PlayerState.IDLE;

    // PlayerController Components
    private CharacterController _characterController;
    private Animator _animator;

    // InputManager CallBack Property
    private Vector2 _move;
    private Vector2 _look;
    private bool _jump;

    enum PlayerState
    {
        IDLE = 0,
        MOVE = 1,
        JUMP = 2,  // TODO
        SIZE
    }

    void Start()
    {
        GetPlayerSettings();
    }

    void Update()
    {
        Move();
        SetPlayerFSM();
        SetPlayerState();
        SetPlayerSettings();
    }

    void LateUpdate()
    {
        RotateBody();
        RotateCamera();
        SetBlendTree();
        SetRenderCamera();
    }

    void SetPlayerSettings()
    {
        // Player Settings
        sensitivity = PlayerManager.Instance.sensitivity;
    }

    void GetPlayerSettings()
    {
        // Player Settings
        sensitivity = PlayerManager.Instance.sensitivity;

        _moveSpeed = PlayerManager.Instance.moveSpeed;
        _gravity = PlayerManager.Instance.gravity;
        _jumpPower = PlayerManager.Instance.jumpPower;
        _maxJumpCount = PlayerManager.Instance.maxJumpCount;

        // Camera Settings
        _mainCamera = PlayerManager.Instance.mainCamera;
        _renderCamera = PlayerManager.Instance.renderCamera;
        _renderCameraX = PlayerManager.Instance.renderCameraX;
        _renderCameraY = PlayerManager.Instance.renderCameraY;
        _renderCameraZ = PlayerManager.Instance.renderCameraZ;

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
        // InputManager CallBack
        _move = InputManager.Instance.move;
        _jump = InputManager.Instance.jump;

        Vector3 moveDir = new Vector3(_move.x, 0, _move.y).normalized;
        moveDir = _mainCamera.transform.TransformDirection(moveDir);

        if (_characterController.collisionFlags == CollisionFlags.Below)
        {
            _yVelocity = 0;
            _currentJumpCount = 0;
        }

        if (_jump && _currentJumpCount < _maxJumpCount)
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
        _look = InputManager.Instance.look;
        _bodyAngle += _look.x * Time.deltaTime * sensitivity;
        this.transform.localEulerAngles = new Vector3(0, _bodyAngle, 0);
    }

    void RotateCamera()
    {
        _look = InputManager.Instance.look;
        _cameraAngle += _look.y * Time.deltaTime * sensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, -30, 30);
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
        _move = InputManager.Instance.move;

        if (_move == Vector2.zero)
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
        _move = InputManager.Instance.move;

        _animator.SetFloat("xDir", _move.x);
        _animator.SetFloat("zDir", _move.y);
    }

    void SetRenderCamera()
    {
        _renderCamera.transform.position = new Vector3(
            (_renderCameraX ? transform.position.x : _renderCamera.transform.position.x),
            (_renderCameraY ? transform.position.y : _renderCamera.transform.position.y),
            (_renderCameraZ ? transform.position.z : _renderCamera.transform.position.z)
        );
    }
}