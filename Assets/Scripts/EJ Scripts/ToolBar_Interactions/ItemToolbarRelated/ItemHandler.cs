using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public List<ItemInformationParent> AllItems;
    // Not Used Yet
    public List<GameObject> AllItemGameObjects;

    #region Singleton
    private static ItemHandler _instance;
    public static ItemHandler Instance { get { return _instance; } }
    #endregion

    void Start()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // This is a check that loops through the list of AllItems it returns the int of how many scripts have the bool IsEquipped enabled.
    // If this returns 2 or more a error is debugged in GetEquippedItems().
    public int IsEquippedCount()
    {
        int Count = 0;
        foreach (ItemInformationParent IIP in AllItems)
        {
            if (IIP.IsEquipped)
            {
                Count++;
            }
        }
        return Count;
    }

    // This returns the script that has the IsEquipped bool checked.
    public ItemInformationParent GetEquippedItem()
    {
        if (IsEquippedCount() >= 2)
        {
            Debug.LogError("MORE THAN 2 EQUIPPED ITEMS!");
            return null;
        }
        foreach (ItemInformationParent IIP in AllItems)
        {
            if (IIP.IsEquipped)
            {
                return IIP;
            }
        }
        return null;
    }
    // If you want the name of the equipped object for some reason. Could be deleted tbh
    public string GetEquippedItemName()
    {
        if (IsEquippedCount() >= 2)
        {
            Debug.LogError("MORE THAN 2 EQUIPPED ITEMS!");
            return null;
        }
        foreach (ItemInformationParent IIP in AllItems)
        {
            if (IIP.IsEquipped)
            {
                return IIP.ItemEnumName.ToString();
            }
        }
        return null;
    }
    // Not Used Yet
    public void AddItem()
    {
        for(int i = 0; i < AllItemGameObjects.Count; i++)
        {
            if(AllItemGameObjects[i].transform.childCount == 0)
            {
                Debug.Log(AllItemGameObjects[i].name);
                return;
            }
        }
    }
}
