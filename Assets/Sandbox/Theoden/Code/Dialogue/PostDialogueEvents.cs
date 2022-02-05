using UnityEngine;
using UnityEngine.Events;
using System;

public class PostDialogueEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject; //The dialogueObject that is linked to the event
	[SerializeField] private UnityEvent postEvent; //Events to be run when the dialogue is finished

	//Getter functions
	public DialogueObject DialogueObject => dialogueObject; 
	public UnityEvent PostEvent => postEvent;
}
