using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "ScriptableObject/GameEventSO")]
public class GameEventSO : ScriptableObject {
    public UnityAction gameEvent;
    public UnityAction<GameObject> eventWithGameObject;

    public void RaiseEvent() {
        gameEvent.Invoke();
    }

    public void RaiseEvent(GameObject obj) {
        eventWithGameObject.Invoke(obj);
    }
}
