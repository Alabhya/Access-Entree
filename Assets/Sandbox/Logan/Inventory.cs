using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Item
{                        
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Dictionary<string,int>  ResourcesRequired {get; set;}
    // Possible Constructor Usage. Ask Logan

}
[CreateAssetMenu(fileName = "New Inventory", menuName="Inventory")]
public class Inventory : ScriptableObject {
    int currentIdCount = 0; // this increments for each new item
    public static Dictionary<string, Item> Items;
    private static string dataPath = "testDb.json"; //tmp db for inventory

    static Inventory()
    {
        Items = new Dictionary<string, Item>(); 
        LoadFromDb();
    }

    public static void LoadFromDb()
    {
        Items = DB.LoadItems<Item>("testDb.json");
    }

    public static void Save()
    {
        DB.SaveItems("testDb.json",Items);
    }

    // What happens if itemName is not in Item Dictionary TryGetKey?

    public static void RemoveItem(String itemName)
    {
        Items.Remove(itemName); 
    }

    public static void RemoveAllItems()
    {
        foreach(var item in Inventory.Items){
            RemoveItem(item.Key);
        }
    }

    public static void Add(String itemName, int num, decimal price)
    {   
        if(Items.ContainsKey(itemName)) {
            Items[itemName].Quantity += num; 
        }
        else {
            Item item = new Item(); 
            item.Id = GetNewId(itemName); 
            item.Name = itemName; 
            item.Quantity = num;
            if(price != null) item.Price = price;
            else item.Price = 0; 
            Items[itemName] = item;
        }
    }

    public static int GetTotalItems() // how many types of items (keys)
    {
        return Items.Count();
    }
    public static int GetItemQuantity(String itemName)
    {
        if(!Items.ContainsKey(itemName)) return 0; 
        return Items[itemName].Quantity;
    }
    public static int GetTotalInventoryQuantity()
    {   
        int count = 0; 
        foreach(var item in Items){
            count += GetItemQuantity(item.Key);
        }
        return count; 
    }
    private static int GetNewId(String itemName)
    {
        int id;
        int currentIdCount = GetTotalItems();
        if (currentIdCount == 0)
            id = 1;
        else
        {
            id = currentIdCount + 1;
        }

        return id;
    }
    public static int ReduceItemCount(String itemName, int reduce) {
        if(Items[itemName].Quantity < reduce) return -1; 
        Items[itemName].Quantity -= reduce; 
        return Items[itemName].Quantity; 
    }
    public static decimal GetItemValue(String itemName)
    {
        decimal value = Items[itemName].Price * Inventory.Items[itemName].Quantity; 
        return value; 
    }
    public static decimal GetInventoryValue()
    {
        decimal value = 0; 
        foreach(var item in Inventory.Items){
            value += GetItemValue(item.Key);
        }
        return value;
    }
    public static void ClearInventory()
    {
        Items.Clear();
    }
}