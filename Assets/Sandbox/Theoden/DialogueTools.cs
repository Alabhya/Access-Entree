using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class DialogueTools : MonoBehaviour
{
	Dictionary<string, object> options;
	List<string> keys;
	
	//Get list of dialogue choices
	private void fillDialogList(int nodeVal)
	{
		options = VD.GetExtraVariables(VD.assigned.assignedDialogue, nodeVal);
		keys = new List<string>(options.Keys);
	}
		
	//Update text in a node
	//This is used to allow for different possibilities based on variables
	private void updateText(int textVal, int nodeVal)
	{
		string newText;
        newText = (string) options[keys[textVal]];
		
        VD.SetComment(VD.assigned.assignedDialogue, nodeVal, 0, newText);
	}
	
	//Pick dialogue randomly from a list
	public void randChoice(int nodeVal)
	{
		fillDialogList(nodeVal); 
		updateText(Random.Range(0, keys.Count), nodeVal);
	}
   
    //Choose dialogue based on given value
	public void specChoice(int choiceVal, int nodeVal)
	{
	   updateText(choiceVal, nodeVal);
	}
}
