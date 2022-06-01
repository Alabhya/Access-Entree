using UnityEngine;

namespace InventorySystem {
    public class InventorySetUp : MonoBehaviour {
        [SerializeField] private Inventory inventoryObj;

        private void OnTriggerEnter(Collider other) {
            InventoryItemObject itemWorldObj = other.GetComponent<InventoryItemObject>();

            InventoryItem tempItemObj = itemWorldObj.GetInventoryItem();

            if (tempItemObj.requiredTool != null) {
                if (inventoryObj.GetItemAmount(tempItemObj.requiredTool) < 1) {
                    Debug.LogFormat("Not Enough {0}", tempItemObj.requiredTool.itemName);
                    return;
                }
            }
                        
            if (itemWorldObj) {
                int itemAmount = itemWorldObj.GetInventoryItemAmount();
                InventoryItem itemSO = itemWorldObj.GetInventoryItem();
                InventoryItem itemObj = new InventoryItem(itemSO.itemType, itemSO.inventoryType, 
                    itemAmount, itemSO.itemName, itemSO.itemImg, itemSO.itemDescription);

                inventoryObj.AddItemInInventory(itemObj);
                itemWorldObj.DestroySelf();
            }
        }
    }
}