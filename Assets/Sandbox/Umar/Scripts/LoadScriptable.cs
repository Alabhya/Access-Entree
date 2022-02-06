using UnityEngine;

public class LoadScriptable : MonoBehaviour
{
    [SerializeField] private ScriptableObject scriptableObject;

    private void Awake()
    {
        DontDestroyOnLoad(scriptableObject);
    }
}
