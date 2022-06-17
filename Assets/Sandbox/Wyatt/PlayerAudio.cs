using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAudio : MonoBehaviour
{

    public FMODUnity.EventReference moveKey = default;
    public FMODUnity.EventReference jumpKey = default;

    private void PlayMovementAudio (FMOD.GUID eventPath, Vector3 positionObj = new Vector3())
    {

        

        FMOD.Studio.EventInstance footStep = FMODUnity.RuntimeManager.CreateInstance(moveKey);
        FMOD.Studio.EventInstance jumpGrunt = FMODUnity.RuntimeManager.CreateInstance(jumpKey);

        if (Input.GetKey("W") || Input.GetKey("A") || Input.GetKey("S") || Input.GetKey("D")) 
        {
            footStep.start();
        }
        else
        {
            footStep.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); 
        }

        if (Input.GetKeyDown("space"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Events/Main Character/jump1"); 
        }

        /* else if(playerInAir == false)
        {
            // we'll get to this later
        }*/

    }




    // private PlayerController playerController;

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
