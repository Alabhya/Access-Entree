using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObj : MonoBehaviour
{
    [SerializeField] private Canvas UIcanvas;
    public GameObject button;
    

    private void Start()
    {
        if (!button) {
            Debug.LogError("button GameObject component missing in InteractionObj.cs object: " + this.name + "\n please assign a button for interaction");
        } else {
            button.SetActive(false); // the button will be deactivated until the player is in activation range of this object
        }
    }

    // Update is called once per frame
    void Update()
    {
        // here we must check if button was pressed and do the interaction
    }
}
