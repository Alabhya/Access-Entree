using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "ItemData", menuName = "InventorySystem/InventoryItem")]
    public class InventoryItem : InventoryItemBase {
        public InventoryItem(ItemType itemTypeObj, InventoryType inventType, int amount, string name, Sprite image, string description) {
            itemType = itemTypeObj;
            inventoryType = inventType;
            itemName = name;
            itemImg = image;
            itemDescription = description;
            itemAmount = amount;
        }
    }

    [System.Serializable]
    public class InventoryItemBase : ScriptableObject {
        public ItemType itemType;
        public InventoryType inventoryType;
        public string itemName;
        public Sprite itemImg;
        public string itemDescription;
        [HideInInspector] public int itemAmount;
    }

}