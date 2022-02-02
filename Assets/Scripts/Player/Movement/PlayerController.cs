using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /**
     * TODO:
     *  Add context sensitive buttons
     *  Reimplement dialogue system with new Game Manager system
     */

    [Header("Player Dialogue")]
	[SerializeField] private DialogueUI _dialogueUI;
    [Space]

    [Header("Movement Values")]
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _gravityMultiplier = -3f;
    // [SerializeField] private bool _isJumping;

    [Header("Ground Detection")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isGrounded;
    private float _groundDistance = 0.4f;
    public bool IsOnFlatGround { get; private set; }

    private PlayerInput _playerInput;
    private CharacterController _controller;
    [SerializeField] private Vector3 _playerVelocity;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.NORMAL)
            PlayerInput();
    }

    /*
     * Move Player
     * - Control the player's movement
     * - Player should only be able to move as long as the Game Manager's
     *   state is set to NORMAL
     **/
    private void PlayerInput()
    {
        //Don't do other actions if dialogue windo is open
        if (_dialogueUI != null && _dialogueUI.IsOpen) return;
        CheckGround();
        MovePlayer();
        PlayerJump(); 
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        _playerVelocity.y = (_isGrounded && _playerVelocity.y < 0) ? 0 : _playerVelocity.y;
        IsOnFlatGround = _isGrounded;
    }

    private void MovePlayer()
    {
        // Translate 2D movement into 3D space
        Vector2 movementInput = _playerInput.Player_Test.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);

        _controller.Move(move * Time.deltaTime * _playerSpeed);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;
    }

    private void PlayerJump()
    {
        // Changes the height position of the player..
        if (_playerInput.Player_Test.Jump.triggered && _isGrounded)
        {
            //_isJumping = true;
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * _gravityMultiplier * _gravityValue);
        }
        else
            //_isJumping = false;

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    /**
     * Commented-out code:
     * 
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _playerVelocity.y < 0) _playerVelocity.y = 0f;
     *
     *  //Changes the height position of the player..
        if (_playerInput.Player_Test.Jump.triggered && _isGrounded)
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
     */
}
