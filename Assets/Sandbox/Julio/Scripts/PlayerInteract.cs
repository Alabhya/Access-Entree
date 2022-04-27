using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class will perform a sphere cast from the player position, to the player's forward direction.
// Once a valid interaction obj is hit we will display an UI button which can be pressed to trigger the custom object interaction logic
public class PlayerInteract : MonoBehaviour
{
    private LayerMask interactionLayer;
    private InteractionObj current;
    public float sphereCastRadius = 0.75f;
    [SerializeField]
    private float sphereCastDistance = 10.0f;

    // this object must contain the button that calls the ButtonPress function below
    public GameObject button;

    private void Start() {
        // setting default values 
        interactionLayer = LayerMask.GetMask("Interaction");
        if (button != null) {
            button.SetActive(false);
        }
        current = null;
    }

    private void Update() {
        if (current) { current.SetOutline(false); } // dissabling any highlights before clearing or changing the current object if any

        RaycastHit hit;
        // this raycast will detect any interactible objects using the Interaction LayerMask
        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.TransformDirection(Vector3.forward), out hit, sphereCastDistance, interactionLayer)) {
            current = hit.transform.GetComponent<InteractionObj>();
            //if (current) {
                // NOOOOOOOOOOOOOOOOOOOOOOOOOO, you cannot reference any specific types of interactible objects in this scrip, because the whole point is polymorphism
                // button.transform.GetChild(0).gameObject.GetComponent<ResourceSetUpUI>().AddRequiredResources(current.GetComponent<BuildableObject>().GetResourcesList(), current.gameObject);
            //}
        } else {
            current = null;
        }
        
        // TODO: CanInteract() function should be used to gray out a button (make it unclickable) instead
        button.SetActive(current != null && current.CanInteract());

        if (current) { current.SetOutline(true); } // highlighting the current object being observed
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.yellow);
    }

    // this function needs to be called from the button object referenced in this class
    public void ButtonPress() {
       // Debug.Log("We pressed the button");
        current.Interaction();
    }
}
