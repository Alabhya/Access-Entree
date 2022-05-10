using UnityEngine;

namespace InventorySystem {
    public class InventorySetUp : MonoBehaviour {
        [SerializeField] private Inventory inventoryObj;

        private void OnTriggerEnter(Collider other) {
            InventoryItemObject itemWorldObj = other.GetComponent<InventoryItemObject>();
            if (itemWorldObj) {
                int itemAmount = itemWorldObj.GetInventoryItemAmount();
                InventoryItem itemSO = itemWorldObj.GetInventoryItem();
                InventoryItem itemObj = new InventoryItem(itemSO.itemObj.itemType, itemSO.itemObj.inventoryType, 
                    itemAmount, itemSO.itemObj.itemName, itemSO.itemObj.itemImg, itemSO.itemObj.itemDescription);

                inventoryObj.AddItemInInventory(itemObj);
                itemWorldObj.DestroySelf();
            }
        }
    }
}