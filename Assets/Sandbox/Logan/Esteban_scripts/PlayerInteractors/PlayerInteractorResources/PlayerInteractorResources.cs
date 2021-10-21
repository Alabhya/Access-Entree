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
    private Outline MyOutline;
    private InteractorController MyInteractorController;
    private Crafting Crafting;

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
    {
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
                    TimeTillDestroy = hit.transform.GetComponent<DropResource>().TimeTillDestroy;
                    if (MyOutline != null)
                    {
                        MyOutline.enabled = false;
                    }

                    MyOutline = hit.transform.GetComponent<Outline>();
                    MyOutline.enabled = true;

                    if (hit.transform.GetComponent<DropResource>() != null)
                    {
                        IsHarvestingResource = true;
                    }
                    else if (hit.transform.GetComponent<TriggerCraft>() != null)
                    {
                        isCrafting = true;
                    }
                }
            }
            else
            {
                IsHarvestingResource = false;
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
                    hit.transform.GetComponent<DropResource>().SpawnResource();
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
                    Crafting = this.GetComponent<Crafting>();
                    string canCraft = Crafting.build("bridge", 1);
                    if (canCraft == "success")
                    {
                        hit.transform.GetComponent<TriggerCraft>().spawnCraft();
                    }
                    else Debug.Log(canCraft);
                    isCrafting = false;

                }
            }
            #endregion
        }
    }
}