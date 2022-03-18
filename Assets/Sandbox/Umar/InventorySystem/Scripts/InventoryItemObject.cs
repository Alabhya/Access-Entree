using UnityEngine;

namespace InventorySystem {
    public class InventoryItemObject : MonoBehaviour {
        [SerializeField] private InventoryItem inventoryItem;

        public InventoryItem GetInventoryItem() {
            return inventoryItem;
        }

        public void DestroySelf() {
            Destroy(gameObject);
        }
    }
}
