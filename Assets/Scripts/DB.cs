using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public static class DB {
    //private static string dataPath = "testDb.json"; //tmp db

    public static Dictionary<string, T> LoadItems<T>(string dataPath)
    {
        Dictionary<string, T> dictOfItems = new Dictionary<string, T>();

        if (File.Exists(dataPath)) {
            string json = File.ReadAllText(dataPath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                dictOfItems = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            }
        };           
        return dictOfItems;
    }        

    public static void SaveItems<T>(string dataPath, Dictionary<string, T> ItemsToSave)
    {   
        if (!File.Exists(dataPath))
            File.Create(dataPath).Dispose();

        string json = JsonConvert.SerializeObject(ItemsToSave);
        File.WriteAllText(dataPath, json);
    }
    
}
