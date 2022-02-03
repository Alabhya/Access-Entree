using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player Controller:
 * - Handles the player's movement controls.
 * - Speed, gravity, ground checks, and recalculated movement vectors are handled here.
 * 
 * TODO:
 * - None at the moment.
 */

public class PlayerController : MonoBehaviour
{
    #region GLOBAL VARIABLES
    [Header("Movement Values")]
    [Tooltip("Up/Down input.")]
    [SerializeField] private float _inputX;

    [Tooltip("Left/Right input.")]
    [SerializeField] private float _inputZ;

    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;

    [Tooltip("The player's gravity value.")]
    [SerializeField] private float _gravityValue = -9.81f;

    [Tooltip("A constant multiplier for the player's gravity. -3f is the default value.")]
    [SerializeField] private float _gravityMultiplier = -3f;

    [Tooltip("The player's current speed.")]
    [SerializeField] private float _currSpeed;

    [Tooltip("The player's move final intended move vector/direction.")]
    [SerializeField] private Vector3 _moveVector;

    [Tooltip("The player's current velocity vector after calculating the intended move vector/direction.")]
    [SerializeField] private Vector3 _playerVelocity;

    private CharacterController _controller;
    private PlayerInput _playerInput;

    [Header("Animator Conditionals")]
    [Tooltip("Use this to trigger specific animations.")]
    public bool BlockPlayerRotation;

    [Tooltip("Set the turn animation of the player in range [0,1].")]
    [Range(0, 1f)] public float DesiredRotationSpeed = 0.3f;
      
    [Tooltip("Set 180 degree turn animation of target in range [0,1].")]
    [Range(0, 1f)] public float AllowPlayerRotation = 0.1f;

    [Tooltip("The current desired move direction relative to the camera's current view position.")]
    public Vector3 DesiredMoveDirection;

    [Header("Ground Detection")]
    [Tooltip("The child transform of the player object. This gameobject's transform should be equal to the player's model's lowest y value.")]
    [SerializeField] private Transform _groundCheck;

    [Tooltip("The LayerMask that defines what is ground for the player. The player will only jump if the interacted object's mask is identical to the player's.")]
    [SerializeField] private LayerMask _groundMask;

    [Tooltip("A flag for the player's current y position.")]
    [SerializeField] private bool _isGrounded;

    [Tooltip("The offset for ground detection. Default value is 0.4f Increase the value for a broader detection. NOTE: If value is set too high, the player's model will not match the collision data!")]
    [SerializeField]
    [Range(0, 1f)] private float _groundDistance = 0.4f;

    public bool IsOnFlatGround { get; private set; }
    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.NORMAL)
            HandlePlayerMovement();
    }

    /*
     * Handle Player Movement:
     * - Handle the order of execution of the player's input.
     * - Player should only be able to move as long as the Game Manager's
     *   state is set to NORMAL.
     * 
     * TODO:
     * - Move Dialogue UI logic to GameManager.GameState.TALKING
     **/
    private void HandlePlayerMovement()
    {   
        CheckPlayerMovementInput();
        CheckGround();
        MovePlayer();
        PlayerJump(); 
    }

    /*
     * Check Player Movement Input:
     * - Handles the initial player movement input.
     * 
     * TODO:
     * - Handle player animations here.
     **/
    private void CheckPlayerMovementInput()
    { 
        _inputX = _playerInput.Player_Test.Move.ReadValue<Vector2>().x;
        _inputZ = _playerInput.Player_Test.Move.ReadValue<Vector2>().y;
        _currSpeed = new Vector2(_inputX, _inputZ).sqrMagnitude;

        if (_currSpeed > AllowPlayerRotation)
            PlayerMoveAndRotation();
    }

    /*
     * Check Ground:
     * - Check if the player is grounded.
     **/
    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        _playerVelocity.y = (_isGrounded && _playerVelocity.y < 0) ? 0 : _playerVelocity.y;
        IsOnFlatGround = _isGrounded;
    }

    /*
     * Move Player:
     * - Apply movement to the player object after calculating the desired move direction.
     **/
    private void MovePlayer()
    {
        _moveVector = new Vector3(0, _playerVelocity.y * 0.2f * Time.deltaTime, 0);
        _controller.Move(_moveVector);
    }

    /*
     * Player Jump:
     * - Check if the player has pressed jump and is able to jump.
     **/
    private void PlayerJump()
    {
        if (_playerInput.Player_Test.Jump.triggered && _isGrounded)
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * _gravityMultiplier * _gravityValue);
        
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    /*
     * Player Move and Rotation:
     * - Handle the player's movement input relative to
     *   the camera's current view position.
     *   
     * - Calculates the player object's rotation.
     **/
    private void PlayerMoveAndRotation()
    {
        _inputX = _playerInput.Player_Test.Move.ReadValue<Vector2>().x;
        _inputZ = _playerInput.Player_Test.Move.ReadValue<Vector2>().y;

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        DesiredMoveDirection = forward * _inputZ + right * _inputX;

        if (!BlockPlayerRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesiredMoveDirection), DesiredRotationSpeed);
            _controller.Move(DesiredMoveDirection * Time.deltaTime * _playerSpeed);
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
