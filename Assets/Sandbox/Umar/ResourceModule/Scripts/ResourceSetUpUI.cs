using UnityEngine;
using InventorySystem;
using System.Collections.Generic;

public class ResourceSetUpUI : MonoBehaviour {
    [SerializeField] private GameObject resourceItemObj;

    List<GameObject> uiObjs = new List<GameObject>();
    GameObject interactedObj = default; //? what is going on here

    public void  AddRequiredResources(List<InventoryItem> itemLists, GameObject objToInteract) {
        if (objToInteract != interactedObj) {
            interactedObj = objToInteract;
            if(uiObjs.Count > 0) { // you don't need to do this because foreach loop will do that implicitly 
                foreach (GameObject obj in uiObjs)
                    Destroy(obj);
            }

            uiObjs?.Clear();

            foreach (InventoryItem itemObj in itemLists) {
                GameObject obj = Instantiate(resourceItemObj, gameObject.transform);
                obj.GetComponent<ResourceGUI>().SetUpResourceUI(itemObj);
                uiObjs.Add(obj);
            }
        }
    }
}
