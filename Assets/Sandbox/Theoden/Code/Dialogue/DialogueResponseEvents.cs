using UnityEngine;
using System;

//Used to allow functions to be called by dialogue system response events
public class DialogueResponseEvents : MonoBehaviour
{
	[SerializeField] private DialogueObject dialogueObject; //The dialogueObject that is linked to the event
	[SerializeField] private ResponseEvent[] rEvents; //Events attached to the given dialogueObject 
	
	//Getter functions
	public DialogueObject DialogueObject => dialogueObject; 
	public ResponseEvent[] R_Events => rEvents;
	
	//Auto fills the response events
	public void OnValidate()
	{
		//Check if dialouge object is valid
		if (dialogueObject == null) return;
		if (dialogueObject.Responses == null) return;
		if (rEvents != null && rEvents.Length == dialogueObject.Responses.Length) return;
		
		if(rEvents == null)
		{
			rEvents = new ResponseEvent[dialogueObject.Responses.Length];
		}
		else
		{
			Array.Resize(ref rEvents, dialogueObject.Responses.Length);
		}
		
		//Go through responses
		for(int i =0; i < dialogueObject.Responses.Length; i++)
		{
			Response response = dialogueObject.Responses[i];
			if(rEvents[i] != null)
			{
				rEvents[i].name = response.ResponseText;
				continue;
			}
			
			rEvents[i] = new ResponseEvent() {name = response.ResponseText};
		}
	}
}
