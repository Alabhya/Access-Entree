using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script respawns the player at a given spot if they exit the level boundary for whatever reason.
*/

public class StayInBounds : MonoBehaviour
{
	[SerializeField] private Vector3 minVals; //The smallest bounds of the level boundary
	[SerializeField] private Vector3 maxVals; //The larges bounds of the level boundary
	[SerializeField] private GameObject player; //The player character
	[SerializeField] private GameObject respawnPoint; //Where the player will respawn should they exit the level boundary
	
	[SerializeField] private SceneTransition sTrans; //The LevelLoader object that teleports the player and determines what transition animation is played.


    // Update is called once per frame
    void Update()
    {
        //Check if the player is within the level bounds.
		//If not, move the player back to the respawn point.
		if(minVals.x > player.transform.position.x || maxVals.x < player.transform.position.x || minVals.y > player.transform.position.y || maxVals.y < player.transform.position.y || minVals.z > player.transform.position.z || maxVals.z < player.transform.position.z)
		{
			Respawn();
		}
    }
	
	//Moves the player to the respawn point
	private void Respawn()
	{
		Debug.Log("Whoopsie doodle: Player out of bounds");
		StartCoroutine(sTrans.ChangeLocation(player, respawnPoint.transform, null));
	}
}
