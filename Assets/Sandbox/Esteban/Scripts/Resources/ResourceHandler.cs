using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public string ObjectToAttachTo = "Resource Handler";
    [Space]
    public ResourceUIController MyResourceUIController;
    #region Singleton
    private static ResourceHandler _instance;
    public static ResourceHandler Instance { get { return _instance; } }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddResource(ResourceType.ResourceName ResourceName)
    {
        switch (ResourceName)
        {
            case ResourceType.ResourceName.None:
                break;
            case ResourceType.ResourceName.Stone:
                MyResourceUIController.AddStone();
                Inventory.Add("stone", 1, 3);
                break;
            case ResourceType.ResourceName.Wood:
                MyResourceUIController.AddWood();
                Inventory.Add("wood", 1, 2);
                break;
            default:
                break;
        }
        
    }

}
