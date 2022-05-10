using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace InventorySystem {
    public class DisplayInventoryUI : MonoBehaviour {
        [SerializeField] private Inventory inventoryObj;
        [SerializeField] private InventoryEventChannelSO uiUpdateSO;
        [SerializeField] private InventoryType inventType;
        [SerializeField] private GameObject itemGUIObjPrefab;

        private List<InventoryGUISetup> inventoryItemObjs = new List<InventoryGUISetup>();

        private void OnEnable() {
            uiUpdateSO.inventoryEvent += UpdateUI;
            UpdateUI(inventoryObj.GetListOfItems());
        }

        private void UpdateUI(Dictionary<ItemType, InventoryItem> inventoryObj) {
            if (inventType == InventoryType.Consumable) {
                for (int i = 0; i < inventoryObj.Count; i++) {
                    if(inventoryObj.ElementAt(i).Value.itemObj.inventoryType == InventoryType.Consumable)
                        UpdateConsumableInventoryItem(inventoryObj.ElementAt(i).Value);
                }
            } else {
                for (int i = 0; i < inventoryObj.Count; i++) {
                    if (inventoryObj.ElementAt(i).Value.itemObj.inventoryType == InventoryType.NonConsumable)
                        UpdateConsumableInventoryItem(inventoryObj.ElementAt(i).Value);
                }
            }
        }

        private void UpdateConsumableInventoryItem(InventoryItem itemObj) {
            bool isItemAvailable = false;
            if (inventoryItemObjs.Count > 0) {
                foreach (InventoryGUISetup obj in inventoryItemObjs) {
                    if (obj.GetItemType() == itemObj.itemObj.itemType) {
                        obj.UpdateInventoryItemAmount(itemObj);
                        isItemAvailable = true;
                    }
                }
            }

            if (!isItemAvailable)
            {
                RectTransform itemSlotRect = Instantiate(itemGUIObjPrefab.transform, gameObject.transform).GetComponent<RectTransform>();
                GameObject itemSlotObj = itemSlotRect.gameObject;
                itemSlotObj.SetActive(true);
                itemSlotObj.GetComponent<InventoryGUISetup>().SetUpInventoryItem(inventoryObj, itemObj);
                inventoryItemObjs.Add(itemSlotObj.GetComponent<InventoryGUISetup>());
            }

        }

        private void OnDisable() {
            uiUpdateSO.inventoryEvent -= UpdateUI;
        }
    }
}
