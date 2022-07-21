using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

[RequireComponent(typeof(InventoryItemObject))]
public class DropResource : InteractionObj
{
    //public string ObjectToAttachTo = "Insert Resource Name Prefab";
    [Space]
    public GameObject resource;
    private int HowManyToSpawn = 5; // TODO: make it so that designer can select a number of resource to spawn instead
    public float TimeTillDestroy;

    public InventoryItem requiredTool;


    void Start()
   {
        base.Start();
        this.transform.rotation = Quaternion.Euler(0,4,0);
    }

    public override void Interaction() {
        SpawnResource();
        Destroy(this);
    }

    public override bool CanInteract() {
        // TODO: CHECK IF THE PLAYER HAS THE CORRECT TOOL TO HARVEST RESOURCE
        // TODO: NEED REF TO TOOL REQUIRED TO HARVEST THIS RESOURCE
        // CHECK IF PLAYER HAS THE SAME TOOL EQUIPED

        // TODO: NEED GAME MANAGER WITH A GLOBAL PLAYER REFERENCE

        //if (requiredTool == gamemanager.getPlayer.getequippedtool()) { return true; } else { return false; }

        return true;
    }

    private void SpawnResource() {
        for (int i = 0; i < HowManyToSpawn; i++)
        {
            float RandomX = Random.Range(-1.5f,1.5f);
            float RandomZ = Random.Range(-1.5f,1.5f);
            GameObject currentResource = Instantiate(resource, this.transform.position, this.transform.rotation);
            currentResource.GetComponent<Rigidbody>().AddForce(RandomX, 0.001f, RandomZ);
        }
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }

    public override void ActivateButtonUI() // NOTE: THESE FUNCTIONS MAY CHANGE IN THE FUTURE
    {
        
        //buttonUI.SetActive(true);
    }

    public override void DissableButtonUI()
    {
        //buttonUI.SetActive(false);
    }

}
