using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class DialogueTools : MonoBehaviour
{
	Dictionary<string, object> options;
	List<string> keys;
	public List<int> multiChoice;
	private int choiceNum = 0;
	
	/////////////////Allows for random dialogue//////////////////////////////
	//Get list of dialogue choices
	private void fillDialogList(int nodeVal)
	{
		options = VD.GetExtraVariables(VD.assigned.assignedDialogue, nodeVal);
		keys = new List<string>(options.Keys);
	}
	
	//Pick dialogue randomly from a list
	public void randChoice(int nodeVal)
	{
		fillDialogList(nodeVal); 
		updateText(Random.Range(0, keys.Count), nodeVal);
	}
	
	
	/////////This is used to allow for different possibilities based on variables///
	//Update text in a node

	private void updateText(int textVal, int nodeVal)
	{
		string newText;
		fillDialogList(nodeVal);
		newText = (string) options[keys[textVal]];
		
        VD.SetComment(VD.assigned.assignedDialogue, nodeVal, 0, newText);
	}
   
    //Choose dialogue based on given value
	public void specChoice(int nodeVal)
	{
		updateText(multiChoice[choiceNum], nodeVal);
	}
	
	public void setMultiChoiceArray(int arrayVal)
	{
		choiceNum = arrayVal;
	}
	
	public void setMultiChoiceValue(
	int choiceVal)
	{
		multiChoice[choiceNum] = choiceVal;
	}
}
