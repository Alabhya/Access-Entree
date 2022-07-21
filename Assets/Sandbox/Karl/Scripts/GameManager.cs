using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    /**
     * Game Manager System
     * Purpose: To manage the overall game state and handle general events
     * 
     * TODO:
     * - Implement state manager
     * - Implement singleton object
     * - ADD PLAYER SINGLETON REFERENCE
     */

    private static GameManager instance;

    private GameManager()
    {
        // initialize your game manager here. Do not reference GameObjects here (i.e. GameObject.Find etc.)
        // because game manager will be created before the objects
        CheckGameState();
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }

    public enum GameState
    { 
        START,
        NORMAL,
        PAUSE,
        TALKING,
        MENU,
    }

    public GameState CurrentState = GameState.START;

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
                Debug.Log("Game State: STARTING LEVEL...");
                CurrentState = GameState.NORMAL;
                break;

            case GameState.NORMAL:
                break;

            case GameState.PAUSE:
                Debug.Log("Game State: Game is currently paused!");
                break;

            case GameState.TALKING:
                Debug.Log("Game State: Player is talking...");
                break;

            case GameState.MENU:
                Debug.Log("Game State: Menu is up! Game world is paused!");
                break;

            default:
                Debug.LogWarning("UNDEFINED/UNKNOWN GAME STATE!");
                break;
        }
    }

    public void ChangeGameStateTo(GameState currentState)
    {
        this.CurrentState = currentState;
    }
}
