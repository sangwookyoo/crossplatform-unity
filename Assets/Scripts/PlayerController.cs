using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerManager Property")]
    private float _moveSpeed;
    private float _gravity;
    private float _jumpPower;
    private int _maxJumpCount;
    private GameObject _camera;

    [Header("Components")]
    private CharacterController _characterController;
    private Animator _animator;

    /////////////////////////////////////////
    private GameObject _playerObject;
    private Vector2 _moveDir;
    private float _yVelocity = 0;
    private int _currentJumpCount = 0;

    private bool _isMoving;
    private bool _isJump = false;



    private PlayerInpuActions _playerInputActions;
    private InputAction _move;
    private InputAction _fire;

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
        SetPlayerSettings();
    }

    void Update()
    {
        Move();
    }

    void LateUpdate()
    {
        SetBlendTree();
        // TODO: Camera
    }

    void SetPlayerSettings()
    {
        // Player Settings
        _moveSpeed = PlayerManager.Instance.moveSpeed;
        _gravity = PlayerManager.Instance.gravity;
        _jumpPower = PlayerManager.Instance.jumpPower;
        _maxJumpCount = PlayerManager.Instance.maxJumpCount;

        // Camera Settings
        _camera = Instantiate(PlayerManager.Instance.MainCamera.gameObject) as GameObject;
        _camera.transform.SetParent(this.transform);
        _camera.transform.localPosition = new Vector3(0f, 1.8f, 0f);

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
        _moveDir = _move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(_moveDir.x, 0, _moveDir.y).normalized;
        moveDir = _camera.transform.TransformDirection(moveDir);

        if (_characterController.collisionFlags == CollisionFlags.Below)
        {
            _yVelocity = 0;
            _currentJumpCount = 0;
            _isJump = false;
        }

        if (Input.GetButtonDown("Jump") && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpCount++;
            _yVelocity = _jumpPower;
            _isJump = true;
        }

// TODO: Animation Root
        _yVelocity += _gravity * Time.deltaTime;
        moveDir.y = _yVelocity;
        _characterController.Move(moveDir * Time.deltaTime * _moveSpeed);
    }

    void SetBlendTree()
    {
        _moveDir = _move.ReadValue<Vector2>();

        _animator.SetFloat("xDir", _moveDir.x);
        _animator.SetFloat("zDir", _moveDir.y);
    }

    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}