using UnityEngine;
using System;

//Used to allow functions to be called by dialogue system
public class DialogueResponseEvents : MonoBehaviour
{
	[SerializeField] private DialogueObject dialogueObject; //The dialogueObject that is linked to the event
	[SerializeField] private ResponseEvent[] events; //Events attached to the given dialogueObject 
	
	//Getter functions
	public DialogueObject DialogueObject => dialogueObject; 
	public ResponseEvent[] Events => events;
	
	//Adds events to the different resoponses
	public void OnValidate()
	{
		//Check if dialouge object is valid
		if (dialogueObject == null) return;
		if (dialogueObject.Responses == null) return;
		if (events != null && events.Length == dialogueObject.Responses.Length) return;
		
		if(events == null)
		{
			events = new ResponseEvent[dialogueObject.Responses.Length];
		}
		else
		{
			Array.Resize(ref events, dialogueObject.Responses.Length);
		}
		
		//Go through responses
		for(int i =0; i < dialogueObject.Responses.Length; i++)
		{
			Response response = dialogueObject.Responses[i];
			
			if(events[i] != null)
			{
				events[i].name = response.ResponseText;
				continue;
			}
			
			events[i] = new ResponseEvent() {name = response.ResponseText};
		}
	}
}
