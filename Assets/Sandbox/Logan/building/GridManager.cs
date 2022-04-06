using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
/**
* Code is from old developer. Has not been implemented in game yet. Still reviewing. 
*/
public class GridCell 
{
    public List<GameObject> BuildingsList;
    public Vector2 grid;
    

    public GridCell(int a, int b)
    {
        grid.x = a;
        grid.y = b;
        BuildingsList = new List<GameObject>();
    }
}

//class to manage a quad tree using a 2D grid
public class GridManager : MonoBehaviour
{
    List<GridCell> GridList;

    //xz plane min and max grid coords for 2D grid
    public Vector2 MinGridCoords;
    public Vector2 MaxGridCoords;
    public Vector2 CellSize;
    public GameObject Player;
    public GameObject UIMANAGER;

    // scale in terms of how many grid cells in any direction should be considered for showing Building Info from the player's current cell
    //()
    public int CellRange;


    //xz plane cell size for each cell in the grid
    private List<int> nearbyBuildingCellIndices;
    private bool SizeValuesValid = false;
    private Transform playerTransform;
    private Vector2Int playerCell;
    private const int FramesToSkip = 5;
    private int skippedFrames = 0;
    private Vector2Int CellCount;

    void Awake() 
    {
        //check if input grid values are good enough to create a gridmap
        if (((MaxGridCoords.x - MinGridCoords.x) >= (2 * CellSize.x)) && ((MaxGridCoords.y - MinGridCoords.y)  >= (2 * CellSize.y)))
        {
            SizeValuesValid = true;
        }
        else 
        {
            Debug.LogError("CURRENT GRID VALUES DO NOT ALLOW FOR CREATION OF A SUITABLE GRID");
        }

        nearbyBuildingCellIndices = new List<int>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AssignToGrid();
        playerTransform = Player.GetComponent<Transform>();


    }


    // Update is called once per frame
    void Update()
    {
        if (--skippedFrames <= 0) 
        {
            skippedFrames = FramesToSkip;
            UpdateDisplays();
        }    
    }

    private void AssignToGrid() 
    {
        if (SizeValuesValid) 
        {
            //get cell count for both directions
            int xCount = (int)Mathf.Ceil(((MaxGridCoords.x - MinGridCoords.x) / CellSize.x));
            int zCount = (int)Mathf.Ceil(((MaxGridCoords.y - MinGridCoords.y) / CellSize.y));
            GridList = new List<GridCell>();
            GridList.Capacity = xCount * zCount;

            CellCount = new Vector2Int(xCount, zCount);

            GridCell cell;
            //instantiate and iterate through each cell and give it a an coordinate ID
            for (int i = 0; i < xCount; ++i)
            {
                for (int j = 0; j < zCount; ++j)
                {
                    cell = new GridCell((int)i, (int)j);
                    GridList.Add(cell);
                }
            }

            //Get all building type objects 
            Building[] temp = GameObject.FindObjectsOfType<Building>();
            //Debug.Log("Object Count : " + temp.Length);
            GameObject[] Buildings = new GameObject[temp.Length];
            for (int i = 0; i < temp.Length; ++i)
            {
                Buildings[i] = temp[i].gameObject;
            }

            float xPos, zPos;
            int gridPosX, gridPosZ;
            Transform currentTransform;
            //assign them to a grid based on their position on the grid
            foreach (GameObject building in Buildings)
            {
                currentTransform = building.GetComponent<Transform>();
                xPos = currentTransform.position.x;
                zPos = currentTransform.position.z;
                gridPosX = (int)((xPos - MinGridCoords.x) / CellSize.x);
                gridPosZ = (int)((zPos - MinGridCoords.y) / CellSize.y);
                GridList[gridPosX * zCount + gridPosZ].BuildingsList.Add(building);
                Debug.Log("Assigned to (" + gridPosX + "," + gridPosZ + ")");
            }
        }
    }

    private void UpdateDisplays() 
    {
        
        if (SizeValuesValid) 
        {
            //get new index for the frame
            Vector2Int newCell = new Vector2Int((int)((playerTransform.position.x - MinGridCoords.x) / CellSize.x), (int)((playerTransform.position.z - MinGridCoords.y) / CellSize.y));

            //if same as already stored index then dont update
            if (newCell != playerCell) 
            {
                //assign to the current cell
                playerCell = newCell;
                GridCell nearbyCell;

                //before clearing the previous list Disable all of their UI's
                foreach (int cellIndex in nearbyBuildingCellIndices) 
                {
                    Assert.IsFalse(cellIndex < 0);
                    nearbyCell = GridList[cellIndex];
                    foreach (GameObject building in nearbyCell.BuildingsList) 
                    {
                        //Debug.Log("Testing canvas = " + (currentCanvas == null));
                        /*if ( building.GetComponent<Building>().hasUI == true) 
                        {
                            building.transform.Find("BuildingUI(Clone)").GetComponent<Canvas>().enabled = false;            
                        }*/
                    }
                }

                nearbyBuildingCellIndices.Clear();

                //get the indices of nearby grids based on players current grid if they exist
                for (int i = -CellRange; i <= CellRange; ++i) 
                {
                    if ((i + playerCell.x >= 0) && (i + playerCell.x < CellCount.x)) 
                    {
                        for (int j = -CellRange; j <= CellRange; ++j)
                        {
                            if ((j + playerCell.y >= 0) && (j + playerCell.y < CellCount.y)) 
                            {
                                nearbyBuildingCellIndices.Add((i + playerCell.x) * CellCount.y + j + playerCell.y);
                            }
                        }
                    }
                }

                Debug.Log("NearbyCellCount = " + nearbyBuildingCellIndices.Count);
                GridCell cell;
                //display info for each building within those cells
                foreach (int index in nearbyBuildingCellIndices) 
                {
                    Assert.IsFalse(index < 0);
                    cell = GridList[index];
                    foreach (GameObject building in cell.BuildingsList) 
                    {
                        //UIMANAGER.GetComponent<UIManager>().SpawnUI(building);
                    }
                }
            }
        }
    }

    private void ClearBuildingsAndCells(List<int> nearbyBuildingCellIndices) 
    {
        //clear building bool flags


    }

    private void DisplayBuildingInfo(GameObject building) 
    {
        Building prop = null;
        //get the script
        prop = building.GetComponent<Building>();
        if (prop == null)
        {
            Debug.LogError("Building Doesnt have Script");
            return;
        }
        else 
        {
            //reactivate UI;
            Debug.Log("Building Name :" + building.name + "; Building Desc: " + prop.Description);
            GameObject buildingCanvas = building.transform.GetChild(0).gameObject;
            buildingCanvas.SetActive(true);

            //update building info
            //buildingCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Text textDesc = buildingCanvas.transform.GetChild(2).GetComponent<Text>();
            textDesc.text = prop.Description;
        }

    }
}
