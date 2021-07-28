using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static void RemoveItem(int productId)
    {
        Inventory.Items.RemoveAll(x => x.Id == productId);
    }

    public static void Add(Item product)
    {
        Items.Add(product);
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
