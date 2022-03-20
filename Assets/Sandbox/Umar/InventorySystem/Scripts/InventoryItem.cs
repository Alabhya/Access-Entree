using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "ItemData", menuName = "InventorySystem/InventoryItem")]
    public class InventoryItem : ScriptableObject {
        public ItemType itemType;
        public InventoryType inventoryType;
        public string itemName;
        public int itemAmount;
        public Sprite itemImg;

        public InventoryItem(ItemType itemTypeObj, InventoryType inventType, int amount, string name, Sprite image) {
            itemType = itemTypeObj;
            inventoryType = inventType;
            itemAmount = amount;
            itemName = name;
            itemImg = image;
        }
    }
}