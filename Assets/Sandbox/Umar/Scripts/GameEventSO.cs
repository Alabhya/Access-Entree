using UnityEngine;
using InventorySystem;
using UnityEngine.Events;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "ScriptableObject/GameEventSO")]
public class GameEventSO : ScriptableObject {
    public UnityAction gameEvent;
    public UnityAction<GameObject> eventWithGameObject;
    public UnityAction<SerializableDictionaryBase<InventoryItem, int>, GameObject> eventForResourceInfo;

    public void RaiseEvent() {
        gameEvent.Invoke();
    }

    public void RaiseEvent(GameObject obj) {
        eventWithGameObject.Invoke(obj);
    }

    public void RaiseEvent(SerializableDictionaryBase<InventoryItem, int> reqItems, GameObject obj) {
        eventForResourceInfo.Invoke(reqItems, obj);
    }
}
