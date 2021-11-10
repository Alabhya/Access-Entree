using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
	//Get UI elements
	[SerializeField] private RectTransform responseBox;
	[SerializeField] private RectTransform responseButtonTemplate;
	[SerializeField] private RectTransform responseContainer;
	
	//Track variables
	private DialogueUI dialogueUI; //Used to control dialogue box
	private ResponseEvent[] responseEvents; //List of response events
	private List<GameObject> tempRespBtn = new List<GameObject>(); //Holds response buttons
	
	private void Start()
	{
		dialogueUI = GetComponent<DialogueUI>(); //Get UI informaiton
	}
	
	//Save responseEvents to this game object
	public void AddResponseEvents(ResponseEvent[] responseEvents)
	{
		this.responseEvents = responseEvents;
	}
	
	//Fills in a list of response buttons
	public void ShowResponses(Response[] responses)
	{
		float responseBoxHeight = 0;
		
		for (int i = 0; i < responses.Length; i++)
		{
			Response response = responses[i];
			int responseIndex = i;
			
			//Put in and set up the button
			GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
			responseButton.gameObject.SetActive(true);
			responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
			responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
			
			tempRespBtn.Add(responseButton);
			
			//Resize the holder
			responseBoxHeight += responseButtonTemplate.sizeDelta.y;
		}
		
		//Update the whole box
		responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
		responseBox.gameObject.SetActive(true);
	}
	
	//Add button functionality for when it is clicked and clear out responses
	private void OnPickedResponse(Response response, int responseIndex)
	{
		//Clear out buttons first
		responseBox.gameObject.SetActive(false);
		foreach(GameObject button in tempRespBtn)
		{
			Destroy(button);
		}
		tempRespBtn.Clear();
		
		//Check if there is a response at the response index. If so, invoke it.
		if(responseEvents != null && responseIndex <= responseEvents.Length)
		{
			responseEvents[responseIndex].OnPickedResponse?.Invoke();
		}
		
		responseEvents = null; //Deallocate choices? Double check, 7 13:50
		
		//If there is a response, wait for player input. Otherwise close DialogueBox
		if(response.DialogueObject)
		{
			dialogueUI.ShowDialogue(response.DialogueObject);
		}
		else
		{
			dialogueUI.CloseDialogueBox();
		}
	}
}
