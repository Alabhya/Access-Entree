using UnityEngine;

namespace InventorySystem {
    public class InventorySetUp : MonoBehaviour {
        [SerializeField] private Inventory inventoryObj;

        private void OnTriggerEnter(Collider other) {
            InventoryItemObject itemWorldObj = other.GetComponent<InventoryItemObject>();
            if (itemWorldObj) {
                int itemAmount = itemWorldObj.GetInventoryItemAmount();
                InventoryItem itemSO = itemWorldObj.GetInventoryItem();
                InventoryItem itemObj = new InventoryItem(itemSO.itemType, itemSO.inventoryType, itemAmount, itemSO.itemName, itemSO.itemImg);

                inventoryObj.AddItemInInventory(itemObj);
                itemWorldObj.DestroySelf();
            }
        }
    }
}