using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "InventoryObj", menuName = "InventorySystem/InventoryObj")]
    public class Inventory : ScriptableObject {
        [SerializeField] private InventoryEventChannelSO uiUpdateSO;
        private Dictionary<ItemType, InventoryItem> itemsList = null;

        public void OnEnable() {
            if(itemsList == null)
                itemsList = new Dictionary<ItemType, InventoryItem>();
            Debug.Log("Inventory SetUped");
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
            if (item.inventoryType == InventoryType.Consumable) {
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
            return itemsList[item.itemType].itemName;
        }

        public int GetItemAmount(InventoryItem item) {
            return itemsList[item.itemType].itemAmount;
        }

        public ItemType GetItemType(InventoryItem item) {
            return itemsList[item.itemType].itemType;
        }

        public Sprite GetItemImg(InventoryItem item) {
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
        Consumable, NonConsumable
    }
}