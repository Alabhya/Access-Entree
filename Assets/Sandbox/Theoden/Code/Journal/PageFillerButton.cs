using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This script fills out the provided layout group with the provided information.
   This script is attached to the desired journal page and takes three variables.
   The "guiElement" is the template of the type of element the journal page is to be filled with.
   The "buttons" are a list of FillerButtonObjects that fill in the provided element type.
   The "holder" is the layout group where all of the elements will be placed.
  */
public class PageFillerButton : MonoBehaviour
{
    [SerializeField] private GameObject btnPrefab;
	[SerializeField] FillerButtonObject[] buttons;
	[SerializeField] private Transform holder;
	
	//Start is called before the first frame update    
	void OnEnable()
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
		foreach (FillerButtonObject button in buttons)
		{
			GameObject tmp = btnPrefab;
			
			//Fill in info
			tmp.GetComponent<CharacterProfileCardFiller>().profile = button.JrnlObj;
			tmp.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = button.BtnImg;
			
			//Create the new element
			GameObject gridElem = Instantiate(tmp) as GameObject;
			
			//Place elements
			gridElem.transform.SetParent(holder, false);
		}
	}
}
