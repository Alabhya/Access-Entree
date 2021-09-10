using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
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
        if (ResourceName == ResourceType.ResourceName.Stone)
            MyResourceUIController.AddStone();
        if (ResourceName == ResourceType.ResourceName.Wood)
            MyResourceUIController.AddWood();
    }

}
