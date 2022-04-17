using UnityEngine;

namespace InventorySystem {
    public class InventoryItemObject : MonoBehaviour {
        [SerializeField] private InventoryItem inventoryItem;
        [SerializeField] private int itemAmount = 1;

        public InventoryItem GetInventoryItem() {
            return inventoryItem;
        }

        public int GetInventoryItemAmount() {
            return itemAmount;
        }

        public void DestroySelf() {
            Destroy(gameObject);
        }
    }
}
