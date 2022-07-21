using UnityEngine;

namespace InventorySystem {
    public class InventorySetUp : MonoBehaviour {
        [SerializeField] private Inventory inventoryObj;

        private InventoryItem heldTool;

        private void OnTriggerEnter(Collider other) {
            InventoryItemObject itemWorldObj = other.GetComponent<InventoryItemObject>();

            InventoryItem tempItemObj = itemWorldObj.GetInventoryItem();

            // TODO: THIS LOGIC WILL BE MOVED TO THE OBJECTS THAT YOU INTERACT WITH
            if (tempItemObj.requiredTool != null)
            {
                if (inventoryObj.GetItemAmount(tempItemObj.requiredTool) < 1)
                {
                    Debug.LogFormat("Not Enough {0}", tempItemObj.requiredTool.itemName);
                    return;
                }
            }

            // TODO: THIS LOGIC SHOULD ONLY AFFECT THE RESOURSE THAT IS PICKED UP, NOT THE OBJECT THAT CAN BE INTERACTED WITH
            if (itemWorldObj)
            {
                int itemAmount = itemWorldObj.GetInventoryItemAmount();
                InventoryItem itemSO = itemWorldObj.GetInventoryItem();
                InventoryItem itemObj = new InventoryItem(itemSO.itemType, itemSO.inventoryType,
                    itemAmount, itemSO.itemName, itemSO.itemImg, itemSO.itemDescription);

                inventoryObj.AddItemInInventory(itemObj);
                itemWorldObj.DestroySelf();
            }
        }

        // TODO: need a function that returns a ref to equipped tool from the player
        public InventoryItem equipedTool()
        {
            return heldTool;
        }

        //GameManager
    }


    

}