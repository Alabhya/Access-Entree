using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour
{
    public GameObject Main_Player;
    public GameObject Main_Camera;
    public Canvas Main_Canvas;

    public static UIManager Instance { get; set; }
    
    void Awake() {
        if (Instance == null) { Instance = this;  }
        else { Destroy(gameObject); }
        Main_Player = GameObject.Find("Player"); // TODO find better way of getting player and camera
        Main_Camera = GameObject.Find("Main Camera");
    }
    
    // Spawns a UI in front of a building
    public GameObject SpawnUIWorld(GameObject obj,GameObject spritePrefab)
    {
        GameObject worldUI = Instantiate(spritePrefab, GetUIPos(obj), spritePrefab.transform.rotation, obj.transform);
        worldUI.transform.SetParent(obj.transform,false);
        worldUI.transform.localScale = new Vector3(0.1f,0.1f,0.1f); // TODO figure out way to ensure scaling matches building
        worldUI.GetComponent<Canvas>().enabled = true;
        worldUI.transform.rotation = Quaternion.LookRotation(worldUI.transform.position - Main_Camera.transform.position);
        return worldUI;
    }
    public GameObject SpawnUIScreen(GameObject spritePrefab)
    {
        GameObject UI = Instantiate(spritePrefab, Main_Canvas.transform.position, spritePrefab.transform.rotation, Main_Canvas.transform);
        UI.transform.SetParent(Main_Canvas.transform,false);
        UI.transform.localScale = new Vector3(1,1,1);
        return UI;
    }
    public Vector3 GetUIPos(GameObject obj)
    {
        Mesh objMesh = obj.GetComponent<MeshFilter>().mesh;
        Vector3 mesh_Extents = objMesh.bounds.extents;
        Transform objtransform = obj.transform;
        Vector3 playerPos = Main_Player.transform.position;

        //this algo works properly only if all buildings with potential UIs are axis aligned
        float Zmin = objtransform.position.z - mesh_Extents.z;
        float Zmax = objtransform.position.z + mesh_Extents.z;

        float Xmin = objtransform.position.x - mesh_Extents.x;
        float Xmax = objtransform.position.x + mesh_Extents.x;

        bool isLessX = (Mathf.Abs(playerPos.x - Xmin) < Mathf.Abs(playerPos.x - Xmax)) ? true : false;
        bool isLessZ = (Mathf.Abs(playerPos.z - Zmin) < Mathf.Abs(playerPos.z - Zmax)) ? true : false;

        Vector3 UIPos = new Vector3((isLessX) ? Xmin : Xmax, objtransform.position.y + mesh_Extents.y + 2, (isLessZ) ? Zmin : Zmax);

        return UIPos;
    }
}
