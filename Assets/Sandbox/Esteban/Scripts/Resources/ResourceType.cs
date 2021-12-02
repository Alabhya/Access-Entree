using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceType : MonoBehaviour
{
    public string ObjectToAttachTo = "Insert Resource Name Prefab";
    public enum ResourceName { None, Stone, Wood}
    [Space]
    public ResourceName MyResourceName;
}
