using UnityEngine;
using InventorySystem;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

public class ResourceRequired {
    public InventoryItem item;
    public int amount;
}

public class ResourceSetUpUI : MonoBehaviour {
    [SerializeField] InventorySystem.Inventory playerInventory;
    [SerializeField] private GameObject resourceItemObj, objToBeShown;

    List<GameObject> uiObjs = new List<GameObject>();
    GameObject interactedObj = default;

    public void  AddRequiredResources(SerializableDictionaryBase<InventoryItem, int> itemLists, GameObject objToInteract) {
        if (objToInteract != interactedObj) {
            interactedObj = objToInteract;
            if (uiObjs.Count > 0) {
                foreach (GameObject obj in uiObjs)
                    Destroy(obj);
            }

            uiObjs?.Clear();

            var listOfResources = itemLists.Keys;
            List<InventoryItem> reqResources = new List<InventoryItem>();
            foreach (InventoryItem itemObj in listOfResources) {
                GameObject obj = Instantiate(resourceItemObj, gameObject.transform);
                obj.GetComponent<ResourceGUI>().SetUpResourceUI(itemObj);
                uiObjs.Add(obj);
                reqResources.Add(itemObj);
            }
            var listOfResourcesAmount = itemLists.Values;
            List<int> reqResourcesAmount = new List<int>();
            foreach (int reqAmount in listOfResourcesAmount) {
                reqResourcesAmount.Add(reqAmount);
            }

            objToBeShown.SetActive(true);
            for (int i = 0; i < reqResources.Count; i++) {
                if (playerInventory.GetItemAmount(reqResources[i]) < reqResourcesAmount[i]) {
                    objToBeShown.SetActive(false);
                    break;
                }
            }

        } else {
        }
    }
}
