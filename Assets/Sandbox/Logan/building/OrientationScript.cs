using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationScript : MonoBehaviour
{
    private Vector3 ForwardVec = new Vector3(0, 0, -1.0f);
    private GameObject main_Player;
    private Vector3 mainPlayerPos;
    private GameObject main_Camera;
    private float currentRotY;
    private LayerMask playerPhysicsLayer;
    //private UIManager UI_Manager;
    private bool playerWasInRange = false;

    public float playerRangeforUIAccessPopUp = 10.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        main_Player = GameObject.Find("Player");
        main_Camera = GameObject.Find("Main Camera");
        if (main_Camera == null) Debug.Log("Camera not Found");
        //UI_Manager  = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (main_Player == null) Debug.Log("Player not Found");
        playerPhysicsLayer = 1 << main_Player.layer;
    }

    // Rotates canvas to face player
    void Update()
    {
        if (GetComponent<Canvas>().enabled == false) return;

        if (CheckIfPlayerInRange() /*&& !UIManager.IsAccessUIbuttonActive*/)
        {
            RaycastHit hit;
            Ray ray = main_Camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~playerPhysicsLayer))
            {
                if (hit.transform.gameObject == this.transform.parent.gameObject)
                {
                    // fire command to activate the UI if the player is facing it
                    //UI_Manager.Enable_UI_For_Accessing_Building();
                    UICameraWork.SetBuidingUIInFocus(this.gameObject);
                }
            }

        }
        else if (!CheckIfPlayerInRange() /*&& UIManager.IsAccessUIbuttonActive*/)
        {
            //UI_Manager.Disable_UI_For_Accessing_Building();
        }
        else if (CheckIfPlayerInRange() /*&& UIManager.IsAccessUIbuttonActive*/) 
        {
            RaycastHit hit;
            Ray ray = main_Camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

            //if ray cast doesnt hit the the building UI to which this script is attached. Deactivate the popup.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~playerPhysicsLayer))
            {
                if (hit.transform.gameObject != this.transform.parent.gameObject)
                {
                    //UI_Manager.Disable_UI_For_Accessing_Building();
                }
            }
            else 
            {
                //UI_Manager.Disable_UI_For_Accessing_Building();
            }
        }
        
        mainPlayerPos = main_Player.transform.position;
        Vector3 diffVec = mainPlayerPos - transform.position;
        //Debug.Log(diffVec);
        diffVec.y = 0;
        //Vector3 norm_DiffVec = diffVec;
        bool IsPlayerXSmallerThanUIX = ((mainPlayerPos.x - transform.position.x) < 0) ? true : false;
        Vector3 normVec = Vector3.Normalize(diffVec);
        float theta = Mathf.Acos(Vector3.Dot(normVec, ForwardVec));
        //Debug.Log(theta);
        transform.eulerAngles = new Vector3( 0.0f ,(((IsPlayerXSmallerThanUIX) ? 1.0f : -1.0f) * theta * (180 / Mathf.PI)),0.0f);
        playerWasInRange = CheckIfPlayerInRange();
    }

    private bool CheckIfPlayerInRange() 
    {
        Vector3 diffVec = mainPlayerPos - this.gameObject.transform.position;
        if (Vector3.Dot(diffVec, diffVec) <= (playerRangeforUIAccessPopUp * playerRangeforUIAccessPopUp))
        {
            //if (GameObject.Find("Canvas").transform.Find("buildingUIAcessPopup").GetComponent<Canvas>().enabled == false) 
            //{

            //}
            //GameObject.Find("ThirdPersonCamera").
            //if ()
            
            return true;
        }

        return false;
    }

    public Vector3 GetForwardVec() 
    {
        return ForwardVec;
    }

}
