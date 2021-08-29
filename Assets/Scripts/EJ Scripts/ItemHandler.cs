using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public List<ItemInformationParent> AllItems;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public ItemInformationParent GetEquippedItem()
    {
        if(IsEquippedCount() >= 2)
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
}
