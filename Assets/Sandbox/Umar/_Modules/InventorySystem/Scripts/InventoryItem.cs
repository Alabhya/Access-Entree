using UnityEngine;

namespace InventorySystem {
    [CreateAssetMenu(fileName = "ItemData", menuName = "InventorySystem/InventoryItem")]
    public class InventoryItem : ScriptableObject {
        public InventoryItemInstance itemObj;

        public InventoryItem(ItemType itemTypeObj, InventoryType inventType, int amount, string name, Sprite image, string description) {
            itemObj = new InventoryItemInstance(itemTypeObj, inventType, amount, name, image, description);
        }
    }

    [System.Serializable]
    public class InventoryItemInstance {
        public ItemType itemType;
        public InventoryType inventoryType;
        public string itemName;
        [HideInInspector] public int itemAmount;
        public Sprite itemImg;
        public string itemDescription;

        public InventoryItemInstance(ItemType itemTypeObj, InventoryType inventType, int amount, string name, Sprite image, string description)
        {
            itemType = itemTypeObj;
            inventoryType = inventType;
            itemAmount = amount;
            itemName = name;
            itemImg = image;
            itemDescription = description;
        }
    }
}