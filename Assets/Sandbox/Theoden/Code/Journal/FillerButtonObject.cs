using UnityEngine;
using UnityEngine.UI;

/* This script creates a filler element scriptable object which provides specific journal objects when pressed.
 */

[CreateAssetMenu(menuName = "FillerButton/FillerButtonObject")]
public class FillerButtonObject : ScriptableObject
{
    //Changeable elements
	[SerializeField] private Sprite btnImg; //The image to show on the button
	[SerializeField] private JournalEntryObject jrnlObj; //The Journal object whose information is used when the button is pressed
	
	//Getters
	public Sprite BtnImg => btnImg;
	public JournalEntryObject JrnlObj => jrnlObj;
}
