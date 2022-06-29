using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryEventChannel", menuName = "InventorySystem/InventoryEvent")]
    public class InventoryEventChannelSO : ScriptableObject {
        public UnityAction<Dictionary<ItemType, InventoryItem>> inventoryEvent;

        public void RaiseEvent(Dictionary<ItemType, InventoryItem> inventoryObj) {
            inventoryEvent.Invoke(inventoryObj);
        }
    }
}