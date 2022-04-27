using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractorResources : MonoBehaviour
{
    #region Public Variables
    public string ObjectToAttachTo = "Player";
    [Space]
    public string LayerName = "Resource";
    public bool IsEnabled = true;
    #endregion
    #region Private Variables
    private LayerMask ResourceLayer;
    private LayerMask CraftingLayer;
    private Outline MyOutline;
    private InteractorController MyInteractorController;

    private float TimeTillDestroy;
    private bool IsHarvestingResource;
    private bool isCrafting;

    private bool DictionaryValueOut;
    private string ThisScriptsName;
    #endregion
    #region Singleton
    private static PlayerInteractorResources _instance;

    public static PlayerInteractorResources Instance { get { return _instance; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Settting Values
        ThisScriptsName = this.GetType().ToString();
        ResourceLayer = LayerMask.GetMask(LayerName);
        CraftingLayer = LayerMask.GetMask("Crafter");

        MyInteractorController = this.GetComponent<InteractorController>();
        MyInteractorController.InteractorBoolActives.Add(ThisScriptsName, true);
        #endregion
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
    {  // why are there so many if statement bro chill
        if (MyInteractorController.InteractorBoolActives.TryGetValue(ThisScriptsName, out DictionaryValueOut))
        {
            IsEnabled = DictionaryValueOut;
        }
        if (IsEnabled == true)
        {
            #region SphereCast
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.75f, transform.TransformDirection(Vector3.forward), out hit, 6, ResourceLayer))
            {   
                if (hit.transform.GetComponent<Outline>() == null)
                    return;
                if (hit.transform.GetComponent<Outline>().enabled == false)
                {
                    for(int i = 0; i < MyInteractorController.InteractorBoolActives.Count; i++)
                    {
                        MyInteractorController.DisableNonEssentialInteractors(ThisScriptsName);
                    }
                    if (MyOutline != null) MyOutline.enabled = false; // disable old outline

                    MyOutline = hit.transform.GetComponent<Outline>();
                    MyOutline.enabled = true; // add new outline

                    if (hit.transform.GetComponent<DropResource>() != null)
                    {
                        TimeTillDestroy = hit.transform.GetComponent<DropResource>().TimeTillDestroy;
                        IsHarvestingResource = true;
                    }
                }
            }
            else if (Physics.SphereCast(transform.position, 0.75f, transform.TransformDirection(Vector3.forward), out hit, 6, CraftingLayer))
            {   
                if (hit.transform.GetComponent<Outline>() == null) return;
                if (hit.transform.GetComponent<Outline>().enabled == false)
                {
                    isCrafting = true;
                }
            }
            else
            {
                IsHarvestingResource = false;
                isCrafting = false;
                if (MyOutline != null)
                {
                    for (int i = 0; i < MyInteractorController.InteractorBoolActives.Count; i++)
                    {
                        MyInteractorController.EnableAllInteractors(ThisScriptsName);
                    }
                    MyOutline.enabled = false;
                    MyOutline = null;
                }
            }
            #endregion
            #region IsHarvestingResource?
            if (IsHarvestingResource)
            {
                TimeTillDestroy -= Time.deltaTime;
                if (TimeTillDestroy <= 0)
                {
                    for (int i = 0; i < MyInteractorController.InteractorBoolActives.Count; i++)
                    {
                        MyInteractorController.EnableAllInteractors(ThisScriptsName);
                    }
                    //hit.transform.GetComponent<DropResource>().SpawnResource();
                    IsHarvestingResource = false;
                }
            }
            #endregion
            #region IsCrafting?
            if (isCrafting)
            {
                MyInteractorController.DisableNonEssentialInteractors(ThisScriptsName);
                TimeTillDestroy -= Time.deltaTime;
                if (TimeTillDestroy <= 0)
                {
                    string[] crafts = new string[] {"bridge" };
                    string canCraft = Crafting.build(crafts[0], 1);
                    if (canCraft == "success")
                    {
                        ItemHandler.Instance.spawnItems(crafts, hit.transform.position);
                    }
                    else Debug.Log(canCraft);
                    isCrafting = false;
                }
            }
            #endregion
        }
    }
}