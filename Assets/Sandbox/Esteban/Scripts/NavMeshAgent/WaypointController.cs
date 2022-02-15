using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    //public bool StartMovement;
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;
    public GameObject CharacterDestinations;

    public float MoveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //targetWaypoint = waypoints[targetWaypointIndex];
        //waypoints[4];
        for(int i = 0; i < CharacterDestinations.transform.childCount; i++)
        {
            Debug.Log("SDF");
            waypoints.Add(CharacterDestinations.transform.GetChild(i).transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetWaypoint != null && waypoints.Count >= 1)
        {

            float MovementStep = MoveSpeed * Time.deltaTime;

            //float distance = Vector3.Distance(this.transform.position, targetWaypoint.position);
            //CheckDistanceToWaypoint(distance);

            transform.position = Vector3.MoveTowards(this.transform.position, targetWaypoint.position, MovementStep);

        }
    }

    public void CheckDistanceToWaypoint(float currentDistance)
    {
        if(currentDistance <= minDistance)
        {
            
            //targetWaypointIndex++;
            //UpdateTargetWaypoint();
        }
    }

    public void UpdateTargetWaypoint()
    {
        //targetWaypoint = waypoints[targetWaypointIndex];
    }

    public void AddWaypoint(Transform WayPointToAdd)
    {
        targetWaypoint = WayPointToAdd;
        waypoints.Add(WayPointToAdd);
    }

}
