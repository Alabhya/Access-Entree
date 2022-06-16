using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBounds : MonoBehaviour
{
	[SerializeField] private Vector3 minVals;
	[SerializeField] private Vector3 maxVals;
	[SerializeField] private Vector3 prefVals;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject respawnPoint;
	
	[SerializeField] private SceneTransition sTrans; //The LevelLoader object that teleports the player and determines what transition animation is played.


    // Update is called once per frame
    void Update()
    {
        //Check x-values
		if(minVals.x > player.transform.position.x || maxVals.x < player.transform.position.x || minVals.y > player.transform.position.y || maxVals.y < player.transform.position.y || minVals.z > player.transform.position.z || maxVals.z < player.transform.position.z)
		{
			Respawn();
		}
    }
	
	private void Respawn()
	{
		Debug.Log("Whoopsie doodle");
		StartCoroutine(sTrans.ChangeLocation(player, respawnPoint.transform, null));
	}
}
