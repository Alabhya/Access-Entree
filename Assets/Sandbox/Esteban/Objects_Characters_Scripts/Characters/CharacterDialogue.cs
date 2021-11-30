using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour

{
    public string ObjectToAttachTo = "Character";
    [SerializeField] DialogueObject _bioObject;
    [SerializeField] DialogueUI _bioUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {
        Debug.Log("IS TALKING");
        // Turn on journal dialogue object
        // FindObjectOfType<DialogueUI>().ShowDialogue(_bioObject);
        _bioUI.ShowDialogue(_bioObject);
    }

}
