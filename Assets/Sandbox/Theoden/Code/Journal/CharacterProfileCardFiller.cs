using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/* This script is used to fill the Character Profile Card element type with information stored
   in JournalEntryObjects.
   
   This script is attached to any 'Character Profile' object (look at the 'Character Profile Template' for an example) and takes seven variables.
   The 'profile'  is a JournalEntryObject that stores the profile information for a given character.
   The 'nameText' variable is where on the 'Character Profile' the given character's name is written.
   The 'profText' variable is where on the 'Character Profile' the given character's profile is written.
   The 'profPic' variable is where on the 'Character Profile' the given character's picture is placed.
   The 'likeGrid' variable is where on the 'Character Profile' the given character's likes are placed.
   The 'dLikeGrid' variable is where on the 'Character Profile' the given character's diislikes are placed.
   The 'greidElemTemp' variable is a template for the like and dislike grid elements to give them a uniform look.
*/

public class CharacterProfileCardFiller : MonoBehaviour
{
	//Profile update information
	public JournalEntryObject profile; //The journal entry object that stores all of hte information about the given character.
	
	//Reference to GUI elements
	[SerializeField] private TMP_Text nameText; //Where the character's name is placed.
	[SerializeField] private TMP_Text profText; //Where the character's profession is placed.
	[SerializeField] private Image profPic;		//Where the character's
	[SerializeField] private Transform likeGrid;//Where the character's likes are placed.
	[SerializeField] private Transform dLikeGrid;//Where the character's dislikes are placed.
	[SerializeField] private Image gridElemTemp; //A template used for the like and dislike grid elements.

    //Start is called before the first frame update    
	void Start()
    {
        //Make sure profile card is fixed
		SetProfile(true);
    }
	
	//Add like and dislike icons
	void AddIcons(Transform pTrans, Icon[] iArray, string nameVar)
	{
		//Clear out old elements
		for (int i=0; i<pTrans.childCount; i++)
		{
			Destroy(pTrans.GetChild(i).gameObject);
		}
		
		//Add new elements
		int x = 0;
		foreach (Icon icon in iArray)
		{
			//Set image and make visible
			Image gridElem = Instantiate(gridElemTemp) as Image;
			
			//Add to grid
			gridElem.transform.SetParent(pTrans, false);
			gridElem.sprite = icon.iconImg;
			
			//Rename Object
			gridElem.name = nameVar+x;
			x++;

			//Make visible
			if(aScoreEventManager.instance.GetEvent(icon.contEvt))
				gridElem.gameObject.SetActive(true);
			else
				gridElem.gameObject.SetActive(false);
		}
	}
	
	//Fill in/update profile information
	void SetProfile(bool fillList)
	{
		//Don't do anything if there is no profile (prevents errors)
		if(profile == null)
		{return;}
		
		string objName = profile.ObjectName;
		
		//Fill in the name
		nameText.text = objName;
		nameText.name = objName + " Name";
		
		//Fill in the Profile
		profText.text = profile.ObjectInfo[0] + "\r\n" + profile.ObjectInfo[1];
		profText.name = objName + " Profile";
		
		//Fill in the Picture
		profPic.sprite = profile.MainPortrait;
		profPic.name = objName + " Picture";
		
		//Only fill in icons in game
		if(fillList)
		{
			//Fill in the Likes and Dislikes
			AddIcons(likeGrid, profile.ObjectIcons[0].iconList, objName+" Likes ");
			AddIcons(dLikeGrid, profile.ObjectIcons[1].iconList, objName+" Dislikes ");
		}
	}
}
