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
    public static List<Item> items;
    public static List<Item> Items
    {
        get
        {
            if (items.Count == 0)
            {
                Load();
            }

            return items;
        }
        set { items = value; }

    }

    static Inventory()
    {
        Items = new List<Item>();
    }

    private static void Load()
    {
        Items = DataManager.LoadItems();
    }

    private static void Save()
    {
        DataManager.SaveItems(Items);
    }

    public static void RemoveItem(int ItemId)
    {
        Inventory.Items.RemoveAll(x => x.Id == ItemId);
    }

    public static void Add(Item Item)
    {
        Items.Add(Item);
    }

    public static int GetNewId()
    {
        int id;
        if (Inventory.Items.Count == 0)
            id = 1;
        else
        {
            id = Inventory.Items.Last().Id + 1;
        }

        return id;
    }

    public static int GetItemCount()
    {
        return Inventory.Items.Count();
    }

    public static int GetUnitCount()
    {
        return Inventory.Items.Select(x => x.Quantity).Sum();
    }

    public static decimal GetInventoryValue()
    {
        return Inventory.Items.Select(x => (x.Price * x.Quantity)).Sum();
    }
    public static void ClearInventory()
    {
        Inventory.Items.Clear();
    }
}
static class DataManager
{
    private static string dataPath = "test.json"; //tmp db

    public static List<Item> LoadItems()
    {
        List<Item> listOfItems = new List<Item>();

        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText("test.json");
            if (!string.IsNullOrWhiteSpace(json))
            {
                listOfItems = JsonConvert.DeserializeObject<List<Item>>(json);
            }
        };           

        return listOfItems;
    }        

    public static void SaveItems(List<Item> ItemsToSave)
    {
        if (!File.Exists(dataPath))
            File.Create(dataPath);

        string json = JsonConvert.SerializeObject(ItemsToSave);

        File.WriteAllText(dataPath, json);
    }
}