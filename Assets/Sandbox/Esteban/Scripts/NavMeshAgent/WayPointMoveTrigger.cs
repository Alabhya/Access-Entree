using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMoveTrigger : MonoBehaviour
{
    private float minDistance = 2f;
    public Transform PlayerMoveTrigger;
    public Transform PlayerMoveTrigger2;
    public Transform Player;
    public WaypointController MyWayPointController;
    private bool Deactive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Deactive)
        {
            if(PlayerMoveTrigger != null)
            {
                float distance = Vector3.Distance(Player.position, PlayerMoveTrigger.position);
                CheckDistanceToWaypoint(distance);
            }
            if (PlayerMoveTrigger2 != null)
            {
                float distance = Vector3.Distance(Player.position, PlayerMoveTrigger2.position);
                CheckDistanceToWaypoint(distance);
            }
        }
    }
    public void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            Debug.Log("TRIGGER");
            MyWayPointController.AddWaypoint(this.transform);
            Deactive = true;
        }
    }
}
