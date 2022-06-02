using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made Julio Delgado

public abstract class InteractionObj : MonoBehaviour
{
    // This reference should be added to the prefabs
    public GameEventSO resourceInfoOpenEvent, resourceInfoCloseEvent; // TODO: Contruct button on start of derived class

    // Definitions for these functions must be defined for each class that derives from it

    protected void Start() {
        // POTENTIAL BUG: The parent object was dissabled, not sure if this happened here or somewhere else.
        //if (buttonUI != null)
        //{
        //    buttonUI.SetActive(false);
        //}
       // Debug.LogWarning("Howdy friend!");
    }

    // This function is called from the UI button that appears when looking at an interation object
    public abstract void Interaction();

    // this function is used to make the button clickable or not, if the player can interact with the object but does not meet the requirements to interact with said object, this shoudl return false
    public abstract bool CanInteract();

    // this function is called by PlayerInteract to display the button UI and activate it
    public abstract void ActivateButtonUI();

    // this function is called by PlayerInteract to dissable/Hide the active button UI
    public abstract void DissableButtonUI();

    // bool isButtonUIActive(); if required this function can be created

    //enables/disables the outline on the objects
    public void SetOutline(bool active) {
        Outline objOutline = gameObject.GetComponent<Outline>();
        if (objOutline == null) { return; }

        objOutline.enabled = active;
    }
}
