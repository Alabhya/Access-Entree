using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCraft : MonoBehaviour
{
    public float buildObjectDistance;
    public GameObject Prefab;
    public Transform spawn;

    public GameObject Player;
    private Vector3 ItemsPosition;
    private Crafting Craft;
    private Vector3 PlayersPosition;
    private Vector3 ReturnPosition;
    private bool built = false;
    // Start is called before the first frame update
    void Awake()
    {
        Craft = this.GetComponent<Crafting>();
    }
    void Start() {
        this.transform.rotation = Quaternion.Euler(0,4,0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayersPosition = Player.transform.position;
        ItemsPosition = this.transform.position;
        //Debug.Log(CalculatePositionX());
        //Debug.Log(CalculatePositionZ());
        if(CalculatePositionX() < buildObjectDistance && CalculatePositionZ() < buildObjectDistance && !built)
        {
            //Crafting.build("bridge",1);
            built = true; 
        }
        if(CalculatePositionX() > buildObjectDistance && CalculatePositionZ() > buildObjectDistance && built) {
            built = false; 
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

    public void spawnCraft()
    {
        GameObject craft = Instantiate(Prefab, spawn.position, this.transform.rotation);
        Vector3 scale = transform.localScale;
        scale.x = 0.01F;
        scale.y = 0.01F;
        scale.z = 0.01F;
        craft.transform.localScale = scale;
        Inventory.Add("cool item", 1, 10); 
        //craft.GetComponent<Rigidbody>().AddForce(new Vector3(RandomX,0,RandomZ));
    }
}
