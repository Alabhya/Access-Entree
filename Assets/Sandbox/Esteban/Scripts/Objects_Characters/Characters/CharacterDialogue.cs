using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CharacterDialogue requires the GameObject to have a DialogueActivator script
[RequireComponent(typeof(DialogueActivator))]

public class CharacterDialogue : MonoBehaviour
{
    public string ObjectToAttachTo = "Character";
    public bool TempActivateMovement;
	private GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
		{
			player = GameObject.FindWithTag("Player");
		}
    }

    public void Talk()
    {
        Debug.Log("IS TALKING");
		if(gameObject.GetComponent("DialogueActivator") != null)
		{
			gameObject.GetComponent<DialogueActivator>().Interact(player.GetComponent<DialogueController>());
		}
    }

}
