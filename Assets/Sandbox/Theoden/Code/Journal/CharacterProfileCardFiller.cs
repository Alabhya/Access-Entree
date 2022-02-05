using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterProfileCardFiller : MonoBehaviour
{
	//Profile update information
	public JournalEntryObject profile;
	
	//Reference to GUI elements
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text profText;
	[SerializeField] private Image profPic;
	[SerializeField] private Transform likeGrid;
	[SerializeField] private Transform dLikeGrid;
	[SerializeField] private Image gridElemTemp;
	
    //Called when something is updated in the inspector
	[ExecuteInEditMode]
	void OnValidate()
	{
		//Update card to reflect changes
		SetProfile(false);
    }

    //Start is called before the first frame update    
	void Start()
    {
        //Make sure profile card is fixed
		SetProfile(true);
    }
	
	//Add like and dislike icons
	void AddSprites(Transform pTrans, Sprite[] sArray)
	{
		//Clear out old elements
		for (int i=0; i<pTrans.childCount; i++)
		{
			Destroy(pTrans.GetChild(i).gameObject);
		}
		
		//Add new elements
		foreach (Sprite sprite in sArray)
		{
			//Set image and make visible
			Image gridElem = Instantiate(gridElemTemp) as Image;
			
			//Add to grid
			gridElem.transform.SetParent(pTrans, false);
			
			gridElem.sprite = sprite;
			gridElem.gameObject.SetActive(true);
		}
	}
	
	//Fill in/update profile information
	void SetProfile(bool fillList)
	{
		//Don't do anything if there is no profile (prevents errors)
		if(profile == null)
		{return;}
		
		//Fill in the name
		nameText.text = profile.ObjectName;
		
		//Fill in the Profile
		profText.text = profile.ObjectInfo[0] + "\r\n" + profile.ObjectInfo[1];
		
		//Fill in the Picture
		profPic.sprite = profile.MainPortrait;
		
		//Only fill in icons in game
		if(fillList)
		{
			//Fill in the Likes and Dislikes
			AddSprites(likeGrid, profile.ObjectIcons[0].iconList);
			AddSprites(dLikeGrid, profile.ObjectIcons[1].iconList);
		}
	}
}
