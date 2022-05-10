using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "InventoryObj", menuName = "InventorySystem/InventoryObj")]
    public class Inventory : ScriptableObject {
        [SerializeField] private InventoryEventChannelSO uiUpdateSO;
        private Dictionary<ItemType, InventoryItem> itemsList = null;

        public void OnEnable() {

            itemsList?.Clear();
            if(itemsList == null)
                itemsList = new Dictionary<ItemType, InventoryItem>();
            Debug.Log("Inventory SetUped");
        }

        public void AddItemInInventory(InventoryItem item) {
            if (itemsList.ContainsKey(item.itemObj.itemType)) {
                AddItemAmount(item);
            } else {
                AddItem(item);
            }
            uiUpdateSO.RaiseEvent(itemsList);
        }

        private void AddItem(InventoryItem item) {
            itemsList.Add(item.itemObj.itemType, item);
        }

        private InventoryItem AddItemAmount(InventoryItem item) {
            if (item.itemObj.inventoryType == InventoryType.Consumable) {
                itemsList[item.itemObj.itemType].itemObj.itemAmount += item.itemObj.itemAmount;
                return itemsList[item.itemObj.itemType];
            } else {
                InventoryItem oldItem = itemsList[item.itemObj.itemType];
                itemsList[item.itemObj.itemType] = item;
                return oldItem;
            }
        }

        public void RemoveItemAmount(InventoryItem item) {
            itemsList[item.itemObj.itemType].itemObj.itemAmount -= item.itemObj.itemAmount;
            uiUpdateSO.RaiseEvent(itemsList);
        }

        public string GetItemName(InventoryItem item) {
            if (itemsList.ContainsKey(item.itemObj.itemType))
                return itemsList[item.itemObj.itemType].itemObj.itemName;
            return null;
        }

        public int GetItemAmount(InventoryItem item) {
            if(itemsList.ContainsKey(item.itemObj.itemType))
                return itemsList[item.itemObj.itemType].itemObj.itemAmount;
            return -1;
        }

        public ItemType GetItemType(InventoryItem item) {
            return itemsList[item.itemObj.itemType].itemObj.itemType;
        }

        public Sprite GetItemImg(InventoryItem item) {
            return itemsList[item.itemObj.itemType].itemObj.itemImg;
        }

        public Dictionary<ItemType, InventoryItem> GetListOfItems() {
            return itemsList;
        }
    }

    public enum ItemType {
        Null, Axe, Sword, Hammer, Wood, Stone, Gold, Herb, Mashroom
    }

    public enum InventoryType {
        Consumable, NonConsumable
    }
}