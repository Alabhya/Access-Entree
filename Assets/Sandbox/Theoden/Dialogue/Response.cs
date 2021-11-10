using UnityEngine;

//Create Response object type
[System.Serializable]
public class Response
{
	//Changeable elements
	[SerializeField] private string responseText;
	[SerializeField] private DialogueObject dialogueObject;
	
	//Getters
	public string ResponseText => responseText;
	public DialogueObject DialogueObject => dialogueObject;
}
