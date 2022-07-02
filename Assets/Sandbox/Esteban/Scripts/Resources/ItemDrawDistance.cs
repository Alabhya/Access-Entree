using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

public class ItemDrawDistance : MonoBehaviour
{
    public string ObjectToAttachTo = "Resource Prefab Drop";
    [Space]
    public float MaximumDrawDistance;
    public float DeleteObjectDistance;
    public float TimeTillPickupable;
    public float GravityStrength;
    #region Private Variables
    private GameObject Player;
    private Vector3 ItemsPosition;
    private PlayerInteractorResources PlayerInteractorScript;
    private ItemHandler MyItemHandler;
   // private ResourceHandler MyResourceHandler;
    private Vector3 PlayersPosition;
    private Vector3 ReturnPosition;
    private bool Pickup;
    private ResourceType MyResourceType;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        MyItemHandler = ItemHandler.Instance;
        Player = PlayerInteractorResources.Instance.gameObject;
        //MyResourceHandler = ResourceHandler.Instance;
        //PlayerInteractorScript = PlayerInteractor.Instance;
        MyResourceType = this.GetComponent<ResourceType>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: YEEEEEEEEEEEEEEEEET ALL OF THIS (just simplify and optimize)
        PlayersPosition = Player.transform.position;
        ItemsPosition = this.transform.position;
        TimeTillPickupable -= Time.deltaTime;
        if (TimeTillPickupable <= 0)
        {
            Pickup = true;
        }
        else
        {
            Vector3 gravity = GravityStrength * 2 * Vector3.down;
            // this.GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
        }
        //Debug.Log(CalculatePositionX());
        //Debug.Log(CalculatePositionZ());

        if (CalculatePositionX() < MaximumDrawDistance && CalculatePositionZ() < MaximumDrawDistance && Pickup)
        {
            transform.position = Vector3.Lerp(this.transform.position, PlayersPosition, 0.1f);
        }
        if (CalculatePositionX() < DeleteObjectDistance && CalculatePositionZ() < DeleteObjectDistance && Pickup)
        {
            //TODO: CALL UMAR'S SCRIPT FROOM HERE LOOOOOL
            // add resource to inventory here 
            //var boi = GetComponent<InventoryItemObject>();
            //if (boi)
                //TODO AGAIN: NEED PLAYER REF
            //GAMEMANAGER.PLAYER.inventoryObj.AddItemInInventory(boi.GetInventoryItem());
            //Destroy(this.gameObject);
        }
    }

    public float CalculatePositionX()
    {
        Vector3 NegativeReturnPosition = PlayersPosition - ItemsPosition;
        ReturnPosition.x = Mathf.Abs(NegativeReturnPosition.x);
        return Mathf.Abs(ReturnPosition.x);
    }
    public float CalculatePositionZ()
    {
        Vector3 NegativeReturnPosition = PlayersPosition - ItemsPosition;
        ReturnPosition.z = Mathf.Abs(NegativeReturnPosition.z);
        return Mathf.Abs(ReturnPosition.z);
    }
}
