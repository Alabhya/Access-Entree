using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class UICameraWork : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameObject Main_Camera = null;
    private static GameObject Main_Player = null;
    private static GameObject buildingUIinFocus = null;
    public static float focalDistance = 8.0f;
    [SerializeField]
    private CinemachineVirtualCamera freeCam; 

    void Start() {
        //turn off all General purpose UI buttons at the start
        //this.gameObject.GetComponent<UIManager>().Disable_UI_For_BACK_BUTTON();
        //this.gameObject.GetComponent<UIManager>().Disable_UI_For_Accessing_Building();

        Main_Camera = GameObject.Find("Main Camera");
        if (Main_Camera == null)
        {   
            Debug.LogError("Cant Find Main Camera Object!!");
        }

        Main_Player = GameObject.Find("Player");
        if (Main_Player == null)
        {
            Debug.LogError("Cant Find Main Player Object!!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void SetBuidingUIInFocus(GameObject UI_IN_FOCUS)
    {
        buildingUIinFocus = UI_IN_FOCUS;
    }

    public void ZoomCameraOnUI()
    {
        if (Main_Player == null)
        {
            Main_Player = GameObject.Find("MainPlayer");
            if (Main_Player == null) Debug.LogError("No Main player available");
        }

        if (Main_Camera == null)
        {
            Main_Camera = GameObject.Find("PlayerCamera");
            if (Main_Camera == null) { Debug.LogError("No Main camera available"); }
        }

        if (Main_Camera == null || Main_Player == null) return;

        Debug.LogWarning("camera zoom called!!");

        //Disable enter Building Popup
        //UIManager.IsAccessUIbuttonAllowed = false;
        //this.gameObject.GetComponent<UIManager>().Disable_UI_For_Accessing_Building();
        //this.gameObject.GetComponent<UIManager>().Enable_UI_For_BACK_BUTTON();

        //disable camera follow and player movement.
        freeCam.Priority = 2; 
        // TODO stop player from moving here (freeze game state)

        //position camera in front of UI
        Vector3 UIFacingDirection = Main_Player.transform.position - buildingUIinFocus.transform.position;
        UIFacingDirection.y = 0.0f;
        UIFacingDirection = Vector3.Normalize(UIFacingDirection);
        freeCam.transform.position = buildingUIinFocus.transform.position + UIFacingDirection * focalDistance /*+ new Vector3(0.0f,(buildingUIinFocus.transform.parent.GetComponent<MeshFilter>().mesh.bounds.extents.y)/2, 0.0f)*/;

        //Look at UI
        freeCam.transform.LookAt(buildingUIinFocus.transform);
    }

    public void ZoomOutFromUI()
    {
        //disable back button and allow access button to popup
        //this.gameObject.GetComponent<UIManager>().Disable_UI_For_BACK_BUTTON();
        //UIManager.IsAccessUIbuttonAllowed = true;

        //allow player movement and camera follow update.
        freeCam.Priority = 0; 
        // TODO allow player to move again
    }
}
