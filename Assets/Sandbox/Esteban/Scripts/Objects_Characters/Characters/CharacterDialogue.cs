using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//CharacterDialogue requires the GameObject to have a DialogueActivator script
[RequireComponent(typeof(DialogueActivator))]

public class CharacterDialogue : MonoBehaviour
{
    public string ObjectToAttachTo = "Character";
    public bool TempActivateMovement;
	private GameObject player;

    public string CharacterBio;
    public string CharacterName;
    public Image CharacterImage;
    public JournalTakeInfo Character;

    public string MissionBio;
    public string MissionName;
    public Image MissionImage;
    public JournalTakeInfo Mission;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
		{
			player = GameObject.FindWithTag("Player");
		}
    }

    public void Talk()
    {
        Debug.Log("IS TALKING");


        if(Character != null)
        {
            Character.Bio(CharacterName, CharacterBio, CharacterImage);
        }


        if(Mission != null)
        {
            Mission.Bio(MissionName, MissionBio, MissionImage);
        }


		if(gameObject.GetComponent("DialogueActivator") != null)
		{
			gameObject.GetComponent<DialogueActivator>().Interact(player.GetComponent<DialogueController>());
		}
    }

}
