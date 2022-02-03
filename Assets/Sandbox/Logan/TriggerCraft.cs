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
    private Vector3 PlayersPosition;
    private Vector3 ReturnPosition;
    private bool built = false;
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
            Crafting.build("axe",1);
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
}
