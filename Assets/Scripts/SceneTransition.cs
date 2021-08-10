using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script plays a scene transition and changes the current scene when certain conditions are fulfilled.
   This script is  attached to the 'Level Loader' object and takes three variables.
   The transition variable is the animation controller or animation override controller that determines which transition animation is played.
   The transitionTime variable is how long the scene waits for the animation before changing. This should be set to the length of the transition animation.
   The levelIndex is the is the index of the scene that is to be switched to. This value can be seen in the 'Scenes In Build' list in the Build Settings.
*/

public class SceneTransition : MonoBehaviour
{
	public Animator transition; //The animation to play when the scene changes
	public float transitionTime = 1f; //How long the transition animation should last
	public int levelIndex; //Which scene to load
	
    // Update is called once per frame
    void Update()
    {
			//For testing. Need to get rid of for final project.
			if(Input.GetMouseButtonDown(0))
			{
				StartCoroutine(LoadLevel());
			}
    }
	
	//Load the provided scene
	IEnumerator LoadLevel()
	{
		//Play animation
		transition.SetTrigger("Start");
		
		//Wait for animation to finish
		yield return new WaitForSeconds(transitionTime);
		
		//Load new scene
		SceneManager.LoadScene(levelIndex);
	}
}
