using UnityEngine;

public class TempUIManager : MonoBehaviour {

    [SerializeField] private GameObject hudPanel, resourceInfoPanel, journalPanel;
    [SerializeField] GameEventSO resourceInfoEvent;

    // Start is called before the first frame update
    void Start() {
        hudPanel.SetActive(true);
        resourceInfoPanel.SetActive(false);
        journalPanel.SetActive(false);
    }



}
