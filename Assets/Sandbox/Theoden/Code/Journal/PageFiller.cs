using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			}
			
			//Create the new element
			GameObject gridElem = Instantiate(tmp) as GameObject;
			
			//Place elements and make sure they are visible
			gridElem.transform.SetParent(holder, false);
			gridElem.SetActive(true);
		}
	}
}
