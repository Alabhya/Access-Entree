using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows player to start a dialogue event
public class DialogueActivator : MonoBehaviour, IInteractable
{
	//The DialogueObject that is opened
	[SerializeField] private DialogueObject dialogueObject;
	
	//Changes the DialogueObject associated with this game object
	public void UpdateDialogueObject(DialogueObject dialogueObject)
	{
		this.dialogueObject = dialogueObject;
	}
	
	//When player enters the trigger area, allow them to open the dialogue
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out DialogueController player))
		{
			player.Interactable = this;
		}
	}
	
	//When player leaves the trigger area, prevent them from opening the dialogue
	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out DialogueController player))
		{
			if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
			{
				player.Interactable = null;
			}
		}
	}
	
	//Allows player to open the dialogue when interact button is pressed
	public void Interact(DialogueController player)
	{
		foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
		{
			if(responseEvents.DialogueObject == dialogueObject)
			{
				player.DialogueUI.AddResponseEvents(responseEvents.Events);
				break;
			}
		}
		
		player.DialogueUI.ShowDialogue(dialogueObject);
	}
}
