using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script fills out the provided layout group with the provided information.
   This script is attached to the desired journal page and takes three variables.
   The "guiElement" is the template of the type of element the journal page is to be filled with.
   The "profiles" are a list of JournalEntryObjects that fill in the provided element type.
   The "holder" is the layout group where all of the elements will be placed.
   
   This script is designed to be expanded. When new guiElement types are made, update the "AddElements()" function's
   switch statement to acount for the new element type.
*/

public class PageFiller : MonoBehaviour
{
	[SerializeField] private GameObject guiElement; //The type of element to fill the list with
	[SerializeField] private JournalEntryObject[] profiles; //The information to fill in the disperate elements
	[SerializeField] private Transform holder; //The layout group to fill
	
	//Start is called before the first frame update    
	void Start()
	{
		//Clear out old elements (prevents duplicates)
		for (int i=0; i<holder.childCount; i++)
		{
			Destroy(holder.GetChild(i).gameObject);
		}
		//Fill in the list
		AddElements();
	}
	
	//Fills the area with the provided information
	void AddElements()
	{
		foreach (JournalEntryObject profile in profiles)
		{
			//Fill in the element information depending on information type
			GameObject tmp = guiElement;
			switch(guiElement.name)
			{
				case "Character Profile Template":
					tmp.GetComponent<CharacterProfileCardFiller>().profile = profile;
					break;
					
				/*More cases can be added here, just make sure to update the documentation*/
			}
			
			//Create the new element
			GameObject gridElem = Instantiate(tmp) as GameObject;
			
			//Place elements and make sure they are visible
			gridElem.transform.SetParent(holder, false);
			gridElem.SetActive(true);
		}
	}
}
