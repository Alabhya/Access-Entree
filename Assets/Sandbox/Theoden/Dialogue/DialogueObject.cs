using UnityEngine;

//Creates the Dialogue object type
[CreateAssetMenu(menuName = "Dialouge/DialogueObject")]
public class DialogueObject : ScriptableObject
{
	[SerializeField] private Dialogue[] dialogueList; //Allows user to add multiple dialogues per object. Can be used for different characters in a conversation.
	[SerializeField] private Response[] responses; //Allows player to respond to dialogue with provided options
	
	public Dialogue[] DialogueList => dialogueList; //A "Getter" that returns a private variable
	
	//Responses
	public bool HasResponses => Responses != null && Responses.Length > 0; //Returns true if there are any responses
	public Response[] Responses => responses;
	
	//Sets default values in Unity Inspector
	void Reset()
	{
		dialogueList = new Dialogue[]
		{
			new Dialogue()
		};
	}
}

//The elements within the dialogue tye
[System.Serializable]
public class Dialogue : ISerializationCallbackReceiver
{
	[TextArea] public string[] text; //The text that is displayed can have many strings per Diaglogue object
	public CharacterTemplateObject charTemplate; //A character template to set standard values
	public Sprite charPortrait; //The portrait displayed with the text
	public AudioClip voice; //The sound that plays when text is typed
	public float textSpeed = 25f; //How quickly text is typed out
	public float pauseTime = 0.6f; //How long text pauses at punctuation.
	
	private CharacterTemplateObject lastCharTemplate; //Tracks the last charTemplate added
	
	//Checks if anything is updated in the inspector
	void OnValidate()
	{
		//If the character template is updated, use the preset template values.
		if(charTemplate != lastCharTemplate)
		{
			lastCharTemplate = charTemplate;
			//Make sure the character template exists first
			if(charTemplate)
			{
				charPortrait  = charTemplate.CharPortrait;
				voice = charTemplate.Voice;
				textSpeed = charTemplate.TextSpeed;
				pauseTime = charTemplate.PauseTime;
			}
			//If template is removed, use default values
			else
			{
				textSpeed = 25f;
				pauseTime = 0.6f;
			}
		}
	}
	
	//Needed for OnValidate to work
	void ISerializationCallbackReceiver.OnBeforeSerialize () => this.OnValidate();
    void ISerializationCallbackReceiver.OnAfterDeserialize () {}
}