using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Crafting
{
    static Dictionary<string, Item> Items = new Dictionary<string, Item>(); 
    
    static Crafting()
    {
        Items = DB.LoadItems<Item>("craftableItems.json"); 
        // if there is no axe in craftableItems, then json has not been created yet
        // for now, we initialize json of craftable itmes with an axe. 
        if(!Items.ContainsKey("axe")) {
            Item axe = new Item(); 
            axe.Id = 1;
            axe.Name = "axe"; 
            axe.Price = 5;  
            axe.Quantity = 1; 
            Dictionary<string,int> axeRequirements = new Dictionary<string,int> {
                {"stone", 2},
                {"wood",1}
            };
            axe.ResourcesRequired = axeRequirements;
            Items["axe"] = axe; 
            DB.SaveItems("craftableItems.json", Items);
        }
    }

    public static bool checkRequirements(string craftName) {
        
        if(!Items.ContainsKey(craftName)) return false; 
        Dictionary<string, int> requirements = new Dictionary<string, int>(); 
        requirements = Items[craftName].ResourcesRequired; 
        
        foreach (var requirementName in requirements.Keys) {

            if( Inventory.GetItemQuantity(requirementName) == 0 )  {
                Debug.Log("need: " + requirementName); 
                return false;
            }
            
            else if(requirements[requirementName] > Inventory.GetItemQuantity(requirementName)) {
                int howMuchNeeded = requirements[requirementName] - Inventory.GetItemQuantity(requirementName); 
                Debug.Log("need " + howMuchNeeded + " more " + requirementName); 
                return false; 
            }
        }
        return true;
    }
    private static void addCraftAsItem(string craftName, decimal craftPrice) {
        Inventory.Add(craftName, 1, craftPrice);
    }
    private static void removeRequirementsFromInventory(string craftName) {
        Dictionary<string, int> requirements = new Dictionary<string, int>(); 
        requirements = Items[craftName].ResourcesRequired;
        foreach (var requirementName in requirements.Keys) {
            // item has been used to build, so remove from inventory
            if(Inventory.GetItemQuantity(requirementName) >= requirements[requirementName]) {
                // item has been used to build, so remove from inventory
                Inventory.ReduceItemCount(requirementName, requirements[requirementName]);
            }
            else { // this should never happen because checkRequirements method should be called first
                Debug.Log("ERROR: Item was built without correct materials");
            }   
            if(Inventory.GetItemQuantity(requirementName) == 0) Inventory.RemoveItem(requirementName);
        }
    }

    public static string build(string craftName, decimal craftPrice) { 
        if(!checkRequirements(craftName)) {
            return "cannot build anything with selected items: ";
        }
        removeRequirementsFromInventory(craftName); 
        addCraftAsItem(craftName, craftPrice);
        Inventory.Save();
        return "success"; 
    } 
}
