using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "ScriptableObject/GameEventSO")]
public class GameEventSO : ScriptableObject {
    public UnityAction gameEvent;

    public void RaiseEvent() {
        gameEvent.Invoke();
    }
}
