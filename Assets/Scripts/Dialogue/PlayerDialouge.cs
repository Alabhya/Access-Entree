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
	
	void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
    }
	
	void Update()
	{
		//Only allow player to move and turn if there are no dialogs loaded
        /*if (!VD.isActive)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * 5, 0);
            float move = Input.GetAxisRaw("Vertical");
            transform.position += transform.forward * 7 * move * Time.deltaTime;
            blue.SetFloat("speed", move);
        }*/
		
		//Temp
		if(Keyboard.current.spaceKey.wasPressedThisFrame)
		{
			TryInteract();
		}
	}
	
	//Casts a ray to see if we hit an NPC and, if so, we interact
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
			Debug.Log("In trigger");
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
