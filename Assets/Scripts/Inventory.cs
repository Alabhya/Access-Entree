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
}

public class Inventory
{
    int currentIdCount = 0; // this increments for each new item
    //public static List<Item> items;
    public static Dictionary<string, Item> Items;  

    static Inventory()
    {
        Items = new Dictionary<string, Item>(); 
    }

    private static void LoadFromDb()
    {
        Items = DataManager.LoadItems();
    }

    private static void Save()
    {
        DataManager.SaveItems(Items);
    }
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

    public static void Add(String itemName, int num)
    {
        Item item = new Item(); 
        item.Name = itemName; 
        item.Quantity = num;
        Items[itemName] = item;
    }

    /*public static int GetNewId(String itemName)
    {
        int id;
        if (GetUnitCount() == 0)
            id = 1;
        else
        {
            id = currentIdCount + 1;
        }

        return id;
    }*/

    public static int GetTotalItems() // how many types of items (keys)
    {
        return Items.Count();
    }
    public static int GetItemQuantity(String itemName)
    {
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
static class DataManager
{
    private static string dataPath = "test.json"; //tmp db

    public static Dictionary<string, Item> LoadItems()
    {
        Dictionary<string, Item> listOfItems = new Dictionary<string, Item>();

        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText("test.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                listOfItems = JsonConvert.DeserializeObject<Dictionary<string, Item>>(json);
            }
        };           

        return listOfItems;
    }        

    public static void SaveItems(Dictionary<string, Item> ItemsToSave)
    {
        if (!File.Exists(dataPath))
            File.Create(dataPath);

        string json = JsonConvert.SerializeObject(ItemsToSave);

        File.WriteAllText(dataPath, json);
    }
}