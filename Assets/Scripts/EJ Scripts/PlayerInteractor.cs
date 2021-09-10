using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public LayerMask ResourceLayer;
    #region Singleton
    private static PlayerInteractor _instance;

    public static PlayerInteractor Instance { get { return _instance; } }
    #endregion
    public Outline MyOutline;

    public float TimeTillDestroy;
    private bool IsMining;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 6, ResourceLayer))
        {
            if(hit.transform.GetComponent<DropResource>() != null && hit.transform.GetComponent<Outline>().enabled == false)
            {
                TimeTillDestroy = 1;
                if (MyOutline != null)
                {
                    MyOutline.enabled = false;
                }
                MyOutline = hit.transform.GetComponent<Outline>();
                MyOutline.enabled = true;
                IsMining = true;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 6, Color.white);
            IsMining = false;
            if(MyOutline != null)
            {
                MyOutline.enabled = false;
                MyOutline = null;
            }
        }

        if (IsMining)
        {
            TimeTillDestroy -= Time.deltaTime;
            if (TimeTillDestroy <= 0)
            {
                hit.transform.GetComponent<DropResource>().SpawnResource();
                IsMining = false;
            }
        }

    }

}

// making things craft have immediate imapact 
