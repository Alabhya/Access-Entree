using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crafting : MonoBehaviour
{
    static Inventory inventory;
    // Add any requirements here as a dictionary, please use format below
    static Dictionary<string, int> bridgeRequirements = new Dictionary<string, int> 
    {     
        {"wood", 2 },
        {"stone", 3 }
    }; 
    static Dictionary<string, int> benchRequirements = new Dictionary<string, int> 
    {     
        {"wood", 3 },
        {"steel", 1 }, 
        {"mana", 1},      
    }; 
    static Dictionary<string, int> woodenSwordRequirements = new Dictionary<string, int> 
    {     
        {"stone", 2 }, 
    }; 
    // these should eventually be scriptable objects accessible to other scripts
    public static Dictionary<string, Dictionary<string,int>> requirements = new Dictionary<string, Dictionary<string,int>>
    {
        {   
            "bridge", // key 
            bridgeRequirements
        },
        {   
            "bench", // key 
            benchRequirements
        },
        {   
            "wooden sword", // key 
            woodenSwordRequirements
        }
    };
    public static class BuildSchemas { // static class doesnt currently serve a point, but may build off of it

        public static bool checkRequirements(string craftName) {
            if(!requirements.ContainsKey(craftName)) return false; 
            
            foreach (var requiredItemName in requirements[craftName].Keys) {

                if( Inventory.GetItemQuantity(requiredItemName) == 0 )  {
                    Debug.Log("need: " + requiredItemName); 
                    return false;
                }
                
                else if(requirements[craftName][requiredItemName] > Inventory.GetItemQuantity(requiredItemName)) {
                    int howMuchNeeded = requirements[craftName][requiredItemName] - Inventory.GetItemQuantity(requiredItemName); 
                    Debug.Log("need " + howMuchNeeded + " more " + requiredItemName); 
                    return false; 
                }
                
            }
            return true;
        }
    }
    public void addCraftAsItem(string craftName, decimal craftPrice) {
        Inventory.Add(craftName, 1, craftPrice);
    }
    void removeRequirementsFromInventory(string craftName) {
 
        foreach (var itemName in requirements[craftName].Keys) {
            //Debug.Log(Inventory.GetItemQuantity(itemName));
            // item has been used to build, so remove from inventory
            if(Inventory.GetItemQuantity(itemName) >= requirements[craftName][itemName]) {
                // item has been used to build, so remove from inventory
                Inventory.ReduceItemCount(itemName, requirements[craftName][itemName]);
            }
            else { // this should never happen because checkRequirements method should be called first
                Debug.Log("ERROR: Item was built without correct materials");
            }   
            if(Inventory.GetItemQuantity(itemName) == 0) Inventory.RemoveItem(itemName);
        }
    }


    public string build(string craftName, decimal craftPrice) { 
        if(!BuildSchemas.checkRequirements(craftName)) {
            return "cannot build anything with selected items: ";
        }
        removeRequirementsFromInventory(craftName); 
        addCraftAsItem(craftName, craftPrice);
        TriggerCraft triggerCraft = this.GetComponent<TriggerCraft>();
        triggerCraft.spawnCraft(); 
        Inventory.Save();
        return "success"; 
    } 

    // Start is called before the first frame update
    void Start()
    {   // add test data into inventory 
        
        Inventory.LoadFromDb(); 

        /*Inventory.Add("wood",3,1); 
        Inventory.Add("steel",3,2); 
        Inventory.Add("mana",1,0); 
        string craftItemName = "bridge"; // tmp for testing
        decimal craftItemPrice = 1; // this data will need to come from database of crafts
        string builtItem = build(craftItemName, craftItemPrice);
        
        Debug.Log("remianing wood: " + Inventory.GetItemQuantity("wood"));
        //Debug.Log("remianing steel: " + Inventory.GetItemQuantity("steel"));
        //Debug.Log("remianing mana: " + Inventory.GetItemQuantity("mana"));
        //Debug.Log("Item built: " + builtItem);
        
        Inventory.Save(); 
        Debug.Log("Inventory saved");*/
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(player.transform.position == this.transform.position){
            build(craftName); 
        }*/
    }
}
