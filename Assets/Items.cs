using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Static singleton instance
    public static Items Instance { get; set; }
    public GameObject sword;
    public GameObject wood;
    public GameObject stone;
    public static Dictionary<string, GameObject> ItemPrefabs = new Dictionary<string, GameObject>(); 
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation

    void Awake()
    {
        Instance = this;
        ItemPrefabs.Add("sword", sword);
        ItemPrefabs.Add("wood", wood);
        ItemPrefabs.Add("stone", stone);
    }
    public GameObject getPrefab(string itemName)
    {
        Debug.Log("I'm doing awesome stuff");
        return ItemPrefabs[itemName];
    }
}