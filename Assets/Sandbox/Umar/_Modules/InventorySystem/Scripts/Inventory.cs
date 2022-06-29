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
        }

        public void AddItemInInventory(InventoryItem item) {
            if (itemsList.ContainsKey(item.itemType)) {
                AddItemAmount(item);
            } else {
                AddItem(item);
            }
            uiUpdateSO.RaiseEvent(itemsList);
        }

        private void AddItem(InventoryItem item) {
            itemsList.Add(item.itemType, item);
        }

        private InventoryItem AddItemAmount(InventoryItem item) {
            if (item.inventoryType == InventoryType.Resources) {
                itemsList[item.itemType].itemAmount += item.itemAmount;
                return itemsList[item.itemType];
            } else {
                InventoryItem oldItem = itemsList[item.itemType];
                itemsList[item.itemType] = item;
                return oldItem;
            }
        }

        public void RemoveItemAmount(InventoryItem item) {
            itemsList[item.itemType].itemAmount -= item.itemAmount;
            uiUpdateSO.RaiseEvent(itemsList);
        }

        public string GetItemName(InventoryItem item) {
            if (itemsList.ContainsKey(item.itemType))
                return itemsList[item.itemType].itemName;
            return null;
        }

        public int GetItemAmount(InventoryItem item) {
            if(itemsList.ContainsKey(item.itemType))
                return itemsList[item.itemType].itemAmount;
            return -1;
        }

        public ItemType GetItemType(InventoryItem item) {
            // This function is not addressing the unavilibility of item
            return itemsList[item.itemType].itemType;
        }

        public Sprite GetItemImg(InventoryItem item) {
            // This function is not addressing the unavilibility of item
            return itemsList[item.itemType].itemImg;
        }

        public Dictionary<ItemType, InventoryItem> GetListOfItems() {
            return itemsList;
        }
    }

    public enum ItemType {
        Null, Axe, Sword, Hammer, Wood, Stone, Gold, Herb, Mashroom
    }

    public enum InventoryType {
        Resources, Tools
    }
}