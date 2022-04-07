using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAudio : MonoBehaviour
{

    // [FMODUnity.EventRef] - turns out this is obsolete

    FMOD.Studio.EventInstance footStep;
    public string jumpGrunt;


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
        if(playerIsMoving == true)
        {
            footStep.start();
        }
        else if(playerIsMoving == false)
        {
            footStep.stop(); // FIX ME
        }

        if(playerInAir == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(jumpGrunt);
        }
        else if(playerInAir == false)
        {
            // we'll get to this later
        }
    }
}
