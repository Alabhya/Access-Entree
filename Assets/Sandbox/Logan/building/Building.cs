using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject UI_Prefab;
    public GameObject UI_3D_Prefab;

    private GameObject building3DUI; 
    private GameObject building2DUI; 

    public GameObject Player; 
    public string Title;
    public string Description;

    [System.NonSerialized]
    public bool UI_2D = false;

    [System.NonSerialized]
    public bool UI_3D = false;

    private GameObject occupant; // change this to character class eventually
    
    void Update()
    {
        if(UI_3D == true) {
            building3DUI.transform.position = UIManager.Instance.GetUIPos(gameObject);
            if(!Check.Nearby(Player,gameObject)){
                removeBuildingUIWorld();
                UI_3D = false;
            }
        }
        else if(Check.Nearby(Player,gameObject)) {
            SpawnBuildingUIWorld(); 
        }

    }
    public void SpawnBuildingUIWorld() {
        Vector3 buildingUIScale = new Vector3(0.1f,0.1f,0.1f);
        building3DUI = UIManager.Instance.SpawnUIWorld(gameObject,UI_3D_Prefab,buildingUIScale);
        UpdateBuildingUI();
        UI_3D = true; 
    }
    void removeBuildingUIWorld() {
        Destroy(building3DUI);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(UI_Prefab == null) Debug.Log("UI prefab not set for building");
        else { 
            Vector3 center = new Vector3(Screen.width/2,Screen.height/2,0);
            building2DUI = UIManager.Instance.SpawnUIScreen(UI_Prefab,center);
            UI_2D = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(building2DUI == null) Debug.Log("UI not in scene");
        if(UI_2D) Destroy(building2DUI);
    }
    public bool UpdateBuildingUI() 
    {
        Transform UITransform = this.transform.Find(UI_3D_Prefab.name + "(Clone)");
        if ( UITransform == null)
        {
            Debug.LogError("Component does not exist or it's name was changed: " + UI_3D_Prefab.name);
            return false;
        }

        Text Title = UITransform.Find("Title").gameObject.GetComponent<Text>();
        Text Description = UITransform.Find("Description").gameObject.GetComponent<Text>();
        Title.text = this.Title;
        Description.text = this.Description;
        return true;
    }
}
