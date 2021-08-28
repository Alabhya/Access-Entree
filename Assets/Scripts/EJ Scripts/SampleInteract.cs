using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleInteract : MonoBehaviour, Interactable
{
    public ItemHandler MyItemHandler;
    public void Interact(ItemHandler ItemHandle, Interactable.ItemName ItemName_)
    {
        // In Item Handler, is their 1 equipped bool checked?
        if (ItemHandle.GetEquippedItem() != null)
        {
            // Does said item have the same name as passed interact() enum variable?
            if (ItemName_.ToString() == ItemHandle.GetEquippedItem().ItemEnumName)
            {
                DoAction();
            }
            else
            {
                Debug.Log(ItemHandle.GetEquippedItem().ItemEnumName);
                Debug.Log(ItemName_.ToString());
                Debug.Log("Wrong object equipped, Name isn't a match");
                //Debug.Log("Is your Interact() enum correct? ItemInformationChild Line 10, Interactable Line 7, Do these enums match? exact wording is key)");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        Interact(MyItemHandler, Interactable.ItemName.Axe);
    }
    private void DoAction()
    {
        // Add your code here...
    }
}
