using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMoveTrigger : MonoBehaviour
{
    private float minDistance = 2f;
    public Transform PlayerMoveTrigger;
    public Transform PlayerMoveTrigger2;
    private Transform Player;
    public Transform NPC; 
    private Transform PlayerCameraTransform;
    private bool Deactive;
    public float MoveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Deactive)
        {
            if(PlayerMoveTrigger != null)
            {
                PlayerMoveTrigger.forward = PlayerCameraTransform.forward;
                float distance = Vector3.Distance(Player.position, PlayerMoveTrigger.position);
                CheckDistanceToWaypoint(distance);
            }
            if (PlayerMoveTrigger2 != null)
            {
                PlayerMoveTrigger2.forward = PlayerCameraTransform.forward;
                float distance = Vector3.Distance(Player.position, PlayerMoveTrigger2.position);
                CheckDistanceToWaypoint(distance);
            }
        }
        else {
            
            float MovementStep = MoveSpeed * Time.deltaTime;
            NPC.position = Vector3.MoveTowards(NPC.position, this.transform.position, MovementStep);
            float distanceFromNPCToWayPoint = Vector3.Distance(NPC.position, PlayerMoveTrigger.position);
            float distanceFromNPCToWayPoint2 = Vector3.Distance(NPC.position, PlayerMoveTrigger2.position);
            if(distanceFromNPCToWayPoint <= minDistance || distanceFromNPCToWayPoint2 <= minDistance) {
                Destroy(gameObject);
            }
        }

    }
    public void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            Deactive = true;
        }
    }
}
