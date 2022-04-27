using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    // Definitions for these functions must be defined for each class that derives from it

    // This function is called from the UI button that appears when looking at an interation object
    public abstract void Interaction();

    // this function is used to make the button clickable or not, if the player can interact with the object but does not meet the requirements to interact with said object, this shoudl return false
    public abstract bool CanInteract();
    
    //enables/disables the outline on the objects
    public void SetOutline(bool active) {
        Outline objOutline = gameObject.GetComponent<Outline>();
        if (objOutline == null) { return; }

        objOutline.enabled = active;
    }
}
