using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterBio : MonoBehaviour
{
    /*
        Character Biography Object
        Displays the NPC's bio information, profile picture, etc.
        Attach script to NPC character

    TODO:
        Locate where Character profile is in project
        
    */

    #region PRIVATE VARIABLES
    [SerializeField] GameObject _journalObject;
    [SerializeField] Image _charPortrait;
    [SerializeField] TMP_Text _bioSummary;
    [SerializeField] Button _backButton;
    #endregion

    #region PUBLIC VARIABLES
    public bool IsOpen { get; private set; }
    #endregion

    private void Start()
    {

        HideBio();
    }

    public void DisplayBio()
    {
        IsOpen = true;
        _journalObject.SetActive(true);
        
        // Stop game time
    }

    public void HideBio()
    {
        IsOpen = false;
        _journalObject.SetActive(false);

        // Resume game time
    }
}
