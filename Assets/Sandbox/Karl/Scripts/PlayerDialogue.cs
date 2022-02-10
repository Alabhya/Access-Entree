using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Player Dialogue:
 * - Handles the player's dialogue controls
 * 
 * TODO:
 * - Implement dialogue logic
 */

public class PlayerDialogue : MonoBehaviour
{
    #region GLOBAL VARIABLES
    [Header("Player Dialogue Control Settings")]
    [SerializeField] private DialogueUI _dialogueUI;
    [SerializeField] private PlayerInput _playerInput; // Might not be needed...?
    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.TALKING)
            HandlePlayerDialogue();
    }

    private void HandlePlayerDialogue()
    { 
        // _playerInput.Player_Test.Interact
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
