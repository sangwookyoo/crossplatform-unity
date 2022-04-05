using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public PlayerDataScriptableObject playerDataScriptableObject;

    private GameObject _playerObject;
    private Vector2 _moveDir;
    private CharacterController _characterController;
    private Animator _animator;
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
        _characterController = this.gameObject.GetComponent<CharacterController>();
        _animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        // BlendAnimation();
    }

    void LateUpdate()
    {
        // TODO: Camera
    }

    public void LoadingPlayer() {
        _playerObject = Instantiate(playerDataScriptableObject.playerObject[0]) as GameObject;

        _playerObject.transform.SetParent(this.transform);
        _playerObject.transform.localScale = new Vector3(1, 1, 1);
        _playerObject.transform.localPosition = Vector3.zero;
        _playerObject.transform.localRotation = Quaternion.identity;
    }

    void SetAnimater() {

    }

    void Move()
    {
        _moveDir = _move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(_moveDir.x, 0, _moveDir.y).normalized;
        _characterController.Move(moveDir * Time.deltaTime * 5);
    }

    void BlendAnimation()
    {
        // _animator.SetBool("isMove", true);

        _moveDir = _move.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(_moveDir.x, 0, _moveDir.y).normalized;

        float h = Input.GetAxis("Horizontal") * Time.deltaTime * 5;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * 5;

        _animator.SetFloat("xDir", h);
        _animator.SetFloat("zDir", v);
    }

    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}
