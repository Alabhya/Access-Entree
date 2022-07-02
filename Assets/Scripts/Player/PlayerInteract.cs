using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Made by Julio Delgado
// this class will perform a sphere cast from the player position, to the player's forward direction.
// Once a valid interaction obj is hit we will display an UI button which can be pressed to trigger the custom object interaction logic
// NOTE: DO NOT REFERENCE ANY CHILD CLASES OF InteractionObj in this class
//

public class PlayerInteract : MonoBehaviour
{
    private LayerMask interactionLayer;
    private InteractionObj current;
    public float sphereCastRadius = 0.75f;
    [SerializeField]
    private float sphereCastDistance = 10.0f;

    // this object must contain the button that calls the ButtonPress function below
    //public GameObject button; // TODO: To be handled by interaction object

    private void Start() {
        // setting default values 
        interactionLayer = LayerMask.GetMask("Interaction");
        current = null;
    }

    private void Update() {
        if (current) { 
            current.SetOutline(false);
            current.DissableButtonUI();
        } // dissabling any highlights and deactivating button UI before clearing or changing the current object if any

        RaycastHit hit;
        // this raycast will detect any interactible objects using the Interaction LayerMask
        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.TransformDirection(Vector3.forward), out hit, sphereCastDistance, interactionLayer)) {
            current = hit.transform.GetComponent<InteractionObj>();
        } else {
            current = null;
        }

        if (current) { 
            current.SetOutline(true);
            current.ActivateButtonUI();
        } // highlighting the current object being observed and activate button UI

#if DEBUG
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * sphereCastDistance, Color.yellow);
#endif
    }

    // this function needs to be called from the button object referenced in this class
    public void ButtonPress() {
        Debug.Log("We pressed the button");
        current.Interaction();
        current.SetOutline(false);
        current.DissableButtonUI();
        current = null;
    }
}
