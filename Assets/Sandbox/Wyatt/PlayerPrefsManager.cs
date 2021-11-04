using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Player;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_GAMMA_KEY = "master_gamma";
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";
    const string HIGH_SCORE = "high_score";
    const string CUTSCENE_PLAYED = "cutscene_played";
    const string PREVIOUS_SCENE = "previous_scene";
    const string NEXT_SCENE = "next_scene";
    Scene scene;
    private string sceneName;


    void Start()
    {

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        SetPreviousScene(sceneName);
        SetCutscenePlayed(0);
    }

    // GAMMA
    public static void SetMasterGamma(float gamma)
    {
        if (gamma >= -1f && gamma <= 1f)
        {
            PlayerPrefs.SetFloat("master_gamma", gamma);
        }
        else
        {
            Debug.LogError("Master Gamma out of range");
        }
    }

    public static float GetMasterGamma()
    {
        return PlayerPrefs.GetFloat(MASTER_GAMMA_KEY);
    }

    // MASTER VOLUME
    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat("master_volume", volume);
        }
        else
        {
            Debug.LogError("Master Volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    // MUSIC VOLUME
    public static void SetMusicVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat("music_volume", volume);
        }
        else
        {
            Debug.LogError("Music Volume out of range");
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    // SFX VOLUME
    public static void SetSfxVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat("sfx_volume", volume);
        }
        else
        {
            Debug.LogError("SFX Volume out of range");
        }
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

    // HIGH SCORE
    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    // CUTSCENE PLAYED
    public static void SetCutscenePlayed(int hasPlayed)
    {
        PlayerPrefs.SetInt(CUTSCENE_PLAYED, hasPlayed);
    }

    public static int GetCutscenePlayed()
    {
        return PlayerPrefs.GetInt(CUTSCENE_PLAYED);
    }

    // PREVIOUS SCENE
    public static void SetPreviousScene(string previousLevel)
    {
        PlayerPrefs.SetString(PREVIOUS_SCENE, previousLevel);
    }

    public static string GetPreviousScene()
    {
        return PlayerPrefs.GetString(PREVIOUS_SCENE);
    }

    // NEXT SCENE
    public static void SetNextScene(string nextLevel)
    {
        PlayerPrefs.SetString(NEXT_SCENE, nextLevel);
    }

    public static string GetNextScene()
    {
        return PlayerPrefs.GetString(NEXT_SCENE);
    }
}
