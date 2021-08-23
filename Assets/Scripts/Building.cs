using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour
{
    public GameObject player; 
    //Inventory Inventory = GameObject.Find("Inventory_Object").GetComponent<Inventory>();
    
    static Dictionary<string, int> bridge = new Dictionary<string, int> 
    {     
        {"wood", 2 },
        {"steel", 3 }, 
        {"mana", 1},      
    }; 
    static Dictionary<string, int> bench = new Dictionary<string, int> 
    {     
        {"wood", 3 },
        {"steel", 1 }, 
        {"mana", 1},      
    }; 
    // these should eventually be static variables accessible to other scripts
    public static Dictionary<string, Dictionary<string,int>> requirements = new Dictionary<string, Dictionary<string,int>>
    {
        {   
            "bridge", // key 
            bridge
        },
                    {   
            "bench", // key 
            bench
        }
    };
    // static class doesnt currently serve a point, but may build off of it
    public static class BuildSchemas {

        public static bool checkRequirements(string buildItemName) {
            if(!requirements.ContainsKey(buildItemName)) return false; 
            
            foreach (var requiredItemName in requirements[buildItemName].Keys) {
                // item has been used to build, so remove from inventory
                
                if( Inventory.GetItemQuantity(requiredItemName) == 0 )  {
                    Debug.Log("need: " + requiredItemName); 
                    return  false;
                }
                
                else if(requirements[buildItemName][requiredItemName] > Inventory.GetItemQuantity(requiredItemName)) {
                    int howMuchNeeded = requirements[buildItemName][requiredItemName] - Inventory.GetItemQuantity(requiredItemName); 
                    Debug.Log("need " + howMuchNeeded + " more " + requiredItemName); 
                    return false; 
                }
                
            }
            return true;
        }
    }

    void removeRequirementsFromInventory(string buildItemName) { 
        foreach (var itemName in requirements[buildItemName].Keys) {
            // item has been used to build, so remove from inventory
            if(Inventory.GetItemQuantity(itemName) >= requirements[buildItemName][itemName]) {
                Inventory.ReduceItemCount(itemName, requirements[buildItemName][itemName]);
            }
            else { // this should never happen because checkRequirements method should be called first
                Debug.Log("ERROR: Item was built without correct materials");
            }   
            if(Inventory.GetItemQuantity(itemName) == 0) Inventory.RemoveItem(itemName);
        }
    }


    string build(string buildItemName) { 
        if(!BuildSchemas.checkRequirements(buildItemName)) {
            return "cannot build anything with selected items: ";
        }
        //removeRequirementsFromInventory(buildItemName); 
        return buildItemName; 
    } 

    // Start is called before the first frame update
    void Start()
    {   // add test data into inventory 
        
        Inventory.Add("wood",4); 
        Inventory.Add("steel",6); 
        Inventory.Add("mana",2); 
        string buildItemName = "bridge"; // tmp for testing
        string builtItem = build(buildItemName);
        
        Debug.Log("remianing wood: " + Inventory.GetItemQuantity("wood"));
        Debug.Log("remianing steel: " + Inventory.GetItemQuantity("steel"));
        Debug.Log("remianing mana: " + Inventory.GetItemQuantity("mana"));
        Debug.Log("Item built: " + builtItem);
        
        Inventory.Save(); 
        Debug.Log("Inventory saved");
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(player.transform.position == this.transform.position){
            build(buildItemName); 
        }*/
    }
}
