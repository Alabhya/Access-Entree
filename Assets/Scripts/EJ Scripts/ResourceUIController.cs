using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUIController : MonoBehaviour
{

    public Text WoodText;
    public int NumberOfWood;
    public Text StoneText;
    public int NumberOfStone;
    // Start is called before the first frame update
    void Start()
    {
        WoodText.text = "Wood: " + NumberOfWood;
        StoneText.text = "Stone: " + NumberOfStone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWood()
    {
        NumberOfWood += 1;
        WoodText.text = "Wood: " + NumberOfWood;
    }

    public void AddStone()
    {
        NumberOfStone += 1;
        StoneText.text = "Stone: " + NumberOfStone;
    }

}
