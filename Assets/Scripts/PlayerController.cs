using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;

    private Vector2 _moveDir;
    private CharacterController _characterController;
    private PlayerInpuActions _playerInputActions;
    private InputAction _move;
    private InputAction _fire;

    private void OnEnable()
    {
        _move = _playerInputActions.Player.Move;
        _move.Enable();

        _fire = _playerInputActions.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }

    private void OnDisable()
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
        _characterController = this.gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        BlendAnimation();
    }

    void LateUpdate()
    {
        // TODO: Camera
    }

    public void Move()
    {
        _moveDir = _move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(_moveDir.x, 0, _moveDir.y).normalized;
        _characterController.Move(moveDir * Time.deltaTime * 5);
    }

    public void BlendAnimation()
    {

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}
