using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObj : MonoBehaviour
{
    public abstract void Interaction();

    public abstract bool CanInteract();
}
