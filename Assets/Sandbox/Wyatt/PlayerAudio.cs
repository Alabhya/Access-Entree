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

        if (playerIsMoving == true)
        {
            footStep.start();
        }
        else if (playerIsMoving == false)
        {
            footStep.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // fixed?
        }

        if (playerInAir == true)
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

    public bool playerIsMoving;
    bool playerInAir;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerIsMoving = playerController.playerMoving;
        playerInAir = playerController.playerJumping;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
