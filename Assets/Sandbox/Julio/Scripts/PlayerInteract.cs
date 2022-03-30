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
    public float RayCastLenght = 0.75f;
    [SerializeField]
    private float rayCastDistance = 10.0f;

    public GameObject button;
    // will need whatever UI element represents a button

    private void Start() {
        interactionLayer = LayerMask.GetMask("Interaction");
        if (button != null) {
            button.SetActive(false);
        }
        current = null;
    }

    private void Update() {
        RaycastHit hit;
        // this raycast will detect any interactible objects
        if (Physics.SphereCast(transform.position, RayCastLenght, transform.TransformDirection(Vector3.forward), out hit, rayCastDistance, interactionLayer)) {
            current = hit.transform.GetComponent<InteractionObj>();
        } else {
            current = null;
        }

        button.SetActive(current != null);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.yellow);
    }

    public void ButtonPress() {
       // Debug.Log("We pressed the button");
        current.Interaction();
    }
}
