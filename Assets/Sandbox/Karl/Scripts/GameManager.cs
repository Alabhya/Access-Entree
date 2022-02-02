using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /**
     * Game Manager System
     * Purpose: To manage the overall game state and handle general events
     * 
     * TODO:
     * - Implement state manager
     * - Implement singleton object
     */

    public static GameManager Instance;

    public enum GameState
    { 
        START,
        NORMAL,
        PAUSE,
        TALKING,
        MENU,
    }

    public GameState CurrentState = GameState.START;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CheckGameState();
    }

    /**
     * Check Game State:
     * - Check the overall game's current state
     * - Use to set BGM audio playback, disable player movement, etc.
     */
    private void CheckGameState()
    {
        switch (CurrentState)
        {
            case GameState.START:
                print("Game State: STARTING LEVEL...");
                CurrentState = GameState.NORMAL;
                break;

            case GameState.NORMAL:
                break;

            case GameState.PAUSE:
                print("Game State: Game is currently paused!");
                break;

            case GameState.TALKING:
                print("Game State: Player is talking...");
                break;

            case GameState.MENU:
                print("Game State: Menu is up! Game world is paused!");
                break;

            default:
                Debug.LogWarning("UNDEFINED/UNKNOWN GAME STATE!");
                break;
        }
    }
}
