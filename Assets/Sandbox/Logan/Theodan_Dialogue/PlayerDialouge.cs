using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VIDE_Data;

public class PlayerDialouge : MonoBehaviour
{	
	public DialogueUIManager dialogUI;
	
	//Stored current VA when inside a trigger
    public VIDE_Assign inTrigger;
	
	//Get VIDE information when you enter an object's trigger
	void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

	//Remove VIDE information when you exit an object's trigger.
    void OnTriggerExit()
    {
        inTrigger = null;
    }
	
	//When Interact button is pressed, try interacting with that object.
	public void GetInteract(InputAction.CallbackContext context)
	{
		TryInteract();
	}
	
	//Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            dialogUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;
            dialogUI.Interact(assigned); //Begins interaction
        }
    }
}
