using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnNearby : MonoBehaviour
{
    [SerializeField] private UnityEvent trigger; // what you want triggered
    public float objectDistance;
    public GameObject Prefab;
    public GameObject Player;
    private Vector3 thisObjectsPosition;
    private Vector3 PlayersPosition;
    private Vector3 ReturnPosition;
    private bool triggered = false;
    public UnityEvent PostTrigger => trigger;

    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        PlayersPosition = Player.transform.position;
        thisObjectsPosition = this.transform.position;
        if(CalculatePositionX() < objectDistance && CalculatePositionZ() < objectDistance && !triggered)
        {
            triggered = true;
            
        }
        if(CalculatePositionX() > objectDistance && CalculatePositionZ() > objectDistance && triggered) {
            triggered = false; 
        }
    }

    public float CalculatePositionX()
    {
        Vector3 NegativeReturnPosition = PlayersPosition - thisObjectsPosition;
        ReturnPosition.x = Mathf.Abs(NegativeReturnPosition.x);
        return Mathf.Abs(ReturnPosition.x);
    }
    public float CalculatePositionZ()
    {
        Vector3 NegativeReturnPosition = PlayersPosition - thisObjectsPosition;
        ReturnPosition.z = Mathf.Abs(NegativeReturnPosition.z);
        return Mathf.Abs(ReturnPosition.z);
    }
}
