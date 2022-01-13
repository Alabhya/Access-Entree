using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script plays a scene transition and either changes the current scene or moves the player somewhere else in the scene when certain conditions are fulfilled.
   This script is  attached to the 'Level Loader' object and takes three variables.
   The transition variable is the animation controller or animation override controller that determines which transition animation is played.
   The transitionTime variable is how long the scene waits for the animation before changing. This should be set to the length of the transition animation.
*/

public class SceneTransition : MonoBehaviour
{
	public Animator transition; //The animation to play when the scene changes
	public float transitionTime = 1f; //How long the transition animation should last

	//Load the provided scene
	public IEnumerator LoadLevel(int newScene)
	{
		//Play animation
		transition.SetTrigger("Start");
		
		//Wait for animation to finish
		yield return new WaitForSeconds(transitionTime);
		
		//Load new scene
		SceneManager.LoadScene(newScene);
	}
	
	//Transport player to provided location
	public IEnumerator ChangeLocation(GameObject player, Transform newSpot, TransitionTrigger tTrigger)
	{
		//Prevent player movement
		player.GetComponent<CharacterController>().enabled = false;
		
		//Play animation
		transition.SetTrigger("Start");

		//Wait for animation to finish
		yield return new WaitForSeconds(transitionTime);
		
		//Move player
		player.transform.localPosition = newSpot.localPosition;
		
		//Finish animation
		transition.SetTrigger("End");
		
		//Wait for animation to finish
		yield return new WaitForSeconds(transitionTime);
		
		//Allow player to move
		player.GetComponent<CharacterController>().enabled = true;

		
		//Turn teleport point back on
		tTrigger.canUse = true;
		tTrigger.curCount = 0;
	}
	
}
