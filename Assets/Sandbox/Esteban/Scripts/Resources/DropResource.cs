using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropResource : InteractionObj
{
    //public string ObjectToAttachTo = "Insert Resource Name Prefab";
    [Space]
    public GameObject resource;
    //public Transform SpawnPosition;
    private int HowManyToSpawn = 5; // TODO: make it so that designer can select a number of resource to spawn instead
    //public enum ResourceType { Wood, Stone }
    public ResourceType TypeOfResource;
    public float TimeTillDestroy;
    // Start is called before the first frame update
   void Start()
   {
        base.Start();
        //this.transform.rotation = Quaternion.Euler(0,4,0); // ? why is this here
        //switch (TypeOfResource)
        //{
        //    case ResourceType.Wood:
        //        HowManyToSpawn = 5;
        //        TimeTillDestroy = 1;
        //        break;
        //    case ResourceType.Stone:
        //        HowManyToSpawn = 4;
        //        TimeTillDestroy = 1.5f;
        //        break;
        //}
    }

    public override void Interaction() {
        SpawnResource();
    }

    public override bool CanInteract() {
        // TODO: CHECK IF THE PLAYER HAS THE CORRECT TOOL TO HARVEST RESOURCE
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
        buttonUI.SetActive(true);
    }

    public override void DissableButtonUI()
    {
        buttonUI.SetActive(false);
    }

}
