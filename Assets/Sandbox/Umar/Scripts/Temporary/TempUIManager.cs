using UnityEngine;
using InventorySystem;
using RotaryHeart.Lib.SerializableDictionary;

public class TempUIManager : MonoBehaviour {

    [SerializeField] private GameObject hudPanel, resourceInfoPanel, journalPanel;
    [SerializeField] private ResourceSetUpUI resourceSetupObj;
    [SerializeField] GameEventSO resourceInfoOpenEvent, resourceInfoCloseEvent;

    private void OnEnable()
    {
        resourceInfoOpenEvent.eventForResourceInfo += ShowResourceInfoPanel;
        resourceInfoCloseEvent.gameEvent += CloseResourceInfoPanel;
    }

    // Start is called before the first frame update
    void Start() {
        hudPanel.SetActive(true);
        resourceInfoPanel.SetActive(false);
        journalPanel.SetActive(false);
    }

    void ShowResourceInfoPanel(SerializableDictionaryBase<InventoryItem, int> reqItems, GameObject interactingObj) {
        //button.transform.GetChild(0).
        //gameObject.GetComponent<ResourceSetUpUI>().
        //AddRequiredResources(current.GetComponent<BuildableObject>().
        //GetResourcesList(), current.gameObject);
        resourceSetupObj.AddRequiredResources(reqItems, interactingObj);
        resourceInfoPanel.SetActive(true);
    }

    void CloseResourceInfoPanel() {
        resourceInfoPanel.SetActive(false);
    }

    private void OnDisable() {
        resourceInfoOpenEvent.eventForResourceInfo -= ShowResourceInfoPanel;
        resourceInfoCloseEvent.gameEvent -= CloseResourceInfoPanel;
    }
}
