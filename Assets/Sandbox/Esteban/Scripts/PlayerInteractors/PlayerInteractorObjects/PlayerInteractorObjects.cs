using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractorObjects : MonoBehaviour
{
    #region Public Variables
    public string ObjectToAttachTo = "Player";
    [Space]
    public string LayerName = "Object";
    public bool IsEnabled = true;
    #endregion
    #region Private Variables
    private LayerMask ResourceLayer;
    private Outline MyOutline;
    private InteractorController MyInteractorController;

    private bool DictionaryValueOut;
    private string ThisScriptsName;
    #endregion
    #region Singleton
    private static PlayerInteractorObjects _instance;

    public static PlayerInteractorObjects Instance { get { return _instance; } }
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
        if(MyInteractorController.InteractorBoolActives.TryGetValue(ThisScriptsName, out DictionaryValueOut))
        {
            IsEnabled = DictionaryValueOut;
        }
        if(IsEnabled == true)
        {
            #region SphereCast
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.75f, transform.TransformDirection(Vector3.forward), out hit, 6, ResourceLayer))
            {
                if (hit.transform.GetComponent<Outline>() == null)
                    return;
                if (hit.transform.GetComponent<Outline>().enabled == false)
                {
                    for (int i = 0; i < MyInteractorController.InteractorBoolActives.Count; i++)
                    {
                        MyInteractorController.DisableNonEssentialInteractors(ThisScriptsName);
                    }
                    hit.transform.GetComponent<ObjectFunction>().DoAction();
                    if (MyOutline != null)
                    {
                        MyOutline.enabled = false;
                    }
                    MyOutline = hit.transform.GetComponent<Outline>();
                    MyOutline.enabled = true;
                }
            }
            else
            {
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
        }
    }
}
