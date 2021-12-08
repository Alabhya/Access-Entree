using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractorController : MonoBehaviour
{
    public Dictionary<string, bool> InteractorBoolActives = new Dictionary<string, bool>();
   
    #region DisableNonEssentialInteractors
    public void DisableNonEssentialInteractors(string CurrentInteractor)
    {
        foreach(KeyValuePair<string, bool> Script in InteractorBoolActives)
        {
            if(Script.Key != CurrentInteractor && Script.Value != false)
            {
                InteractorBoolActives[Script.Key] = false;
                return;
            }
        }
    }
    #endregion

    #region EnableAllInteractors
    public void EnableAllInteractors(string CurrentInteractor)
    {
        foreach (KeyValuePair<string, bool> Script in InteractorBoolActives)
        {
            if (Script.Key != CurrentInteractor && Script.Value == false)
            {
                InteractorBoolActives[Script.Key] = true;
                return;
            }
        }
    }
    #endregion
}
