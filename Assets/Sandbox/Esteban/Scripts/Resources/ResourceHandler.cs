using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHandler : MonoBehaviour
{
    //public string ObjectToAttachTo = "UI";
    [Space]
    public Text WoodText;
    public int NumberOfWood;
    public Text StoneText;
    public int NumberOfStone;

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
        WoodText.text = "Wood: " + NumberOfWood;
        StoneText.text = "Stone: " + NumberOfStone;
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
        WoodText.text = "Wood: " + Inventory.GetItemQuantity("wood");
        StoneText.text = "Stone: " + Inventory.GetItemQuantity("stone");
    }


    public void AddResource(ResourceType.ResourceName ResourceName)
    {
        switch (ResourceName)
        {
            case ResourceType.ResourceName.None:
                {
                    break;
                }
            case ResourceType.ResourceName.Stone:
                {
                    AddStone();
                    Inventory.Add("stone", 1, 3);
                    break;
                }
            case ResourceType.ResourceName.Wood:
                {
                    AddWood();
                    Inventory.Add("wood", 1, 2);
                    break;
                }
            default:
                {
                    break;
                }
        }
        
    }

    public void AddWood()
    {
        NumberOfWood += 1;
        // WoodText.text = "Wood: " + NumberOfWood;
    }


    public void AddStone()
    {
        NumberOfStone += 1;
        // StoneText.text = "Stone: " + NumberOfStone;
    }

}
