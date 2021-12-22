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
        return ItemPrefabs[itemName];
    }
    public void spawnItems(string[] itemNames,Vector3 spawnPosition)
    {
        
        spawnPosition = spawnPosition + new Vector3(5, 0, 5); // move spawned object over
        Quaternion spawnRotation = Quaternion.identity; // default rotation
        foreach (string itemName in itemNames)
        {
            GameObject item = Items.Instance.getPrefab(itemName);
            GameObject spawnedItem = Instantiate(item, spawnPosition, spawnRotation);
        }
    }
}