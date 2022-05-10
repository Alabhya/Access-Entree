using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem {
    public class InventoryGUISetup : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Image itemImg;

        private Inventory inventoryObj;
        private ItemType itemTypeOfObj;

        public void SetUpInventoryItem(Inventory inventory, InventoryItem inventoryItem) {
            inventoryObj = inventory;
            itemTypeOfObj = inventoryItem.itemObj.itemType;
            RefreshInventoryItem(inventoryItem);
        }

        public void UpdateInventoryItemAmount(InventoryItem inventoryItem) {
            //inventoryObj.AddItemAmount(inventoryItem);
            RefreshInventoryItem(inventoryItem);
        }

        public void RefreshInventoryItem(InventoryItem inventoryItem) {
            itemName.text = inventoryObj.GetItemName(inventoryItem);
            itemAmount.text = inventoryObj.GetItemAmount(inventoryItem).ToString();
            itemImg.sprite = inventoryObj.GetItemImg(inventoryItem);
        }

        public ItemType GetItemType() {
            return itemTypeOfObj;
        }

        //public int GetItemAmount() {
        //    return inventoryItemObj.itemAmount;
        //}
    }
}