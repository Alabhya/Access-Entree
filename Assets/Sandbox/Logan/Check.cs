using UnityEngine;
using UnityEngine.Events;

public static class Check 
{
    //[SerializeField] private UnityEvent trigger; // what you want triggered on nearby
    //public GameObject Player;
    //private static bool triggered = false;
    public static float objectDistance = 15; // TODO consider making class non-static and making this an instance variable

    // Update is called once per frame
    public static bool Nearby(GameObject player, GameObject current)
    {
        Vector3 PlayersPosition = player.transform.position;
        Vector3 thisObjectsPosition = current.transform.position;
        if(CalculatePositionX(PlayersPosition,thisObjectsPosition) < objectDistance && CalculatePositionZ(PlayersPosition,thisObjectsPosition) < objectDistance)
        {
            //triggered = true;
            //trigger.Invoke();
            return true;
        }
        else {
            return false;
        }
    }

    private static float CalculatePositionX(Vector3 PlayersPosition, Vector3 thisObjectsPosition)
    {
        Vector3 NegativeReturnPosition = PlayersPosition - thisObjectsPosition;
        Vector3 ReturnPosition; 
        ReturnPosition.x = Mathf.Abs(NegativeReturnPosition.x);
        return Mathf.Abs(ReturnPosition.x);
    }
    private static float CalculatePositionZ(Vector3 PlayersPosition, Vector3 thisObjectsPosition)
    {
        Vector3 NegativeReturnPosition = PlayersPosition - thisObjectsPosition;
        Vector3 ReturnPosition;
        ReturnPosition.z = Mathf.Abs(NegativeReturnPosition.z);
        return Mathf.Abs(ReturnPosition.z);
    }
}
