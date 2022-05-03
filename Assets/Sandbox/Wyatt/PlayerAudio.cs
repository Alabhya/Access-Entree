using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAudio : MonoBehaviour
{

    private void PlayMovementAudio (FMOD.GUID eventPath)
    {
        FMOD.Studio.EventInstance footStep = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMOD.Studio.EventInstance jumpGrunt = FMODUnity.RuntimeManager.CreateInstance(eventPath);

        if (Input.GetKey("W") || Input.GetKey("A") || Input.GetKey("S") || Input.GetKey("D")) 
        {
            footStep.start();
        }
        else
        {
            footStep.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // fixed?
        }

        if (Input.GetKeyDown("space"))
        {
            jumpGrunt.start();
            jumpGrunt.release();
        }

        /* else if(playerInAir == false)
        {
            // we'll get to this later
        }*/

    }




    private PlayerController playerController;

    // public bool playerIsMoving;
    // bool playerInAir;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
