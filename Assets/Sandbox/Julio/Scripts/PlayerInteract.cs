using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class will perform a sphere cast from the player position, to the player's forward direction.
// Once a valid interaction obj is hit we will display an UI button which can be pressed to trigger the custom object interaction logic
public class PlayerInteract : MonoBehaviour
{
    private LayerMask interactionLayer;
    private InteractionObj current;
    // will need whatever UI element represents a button

    private void Start() {
        interactionLayer = LayerMask.GetMask("Interaction");

        current = null;
    }

    private void Update() {
        if (current != null) {
            // activate interaction button
        } else {
            // deactivate interaction button
        }
    }

}
