using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAxeSwing : MonoBehaviour, Interactable
{
    private ItemHandler MyItemHandler;
    public void InteractTools(ItemHandler ItemHandle, Interactable.ItemName ItemName_)
    {
        // In Item Handler, is their 1 equipped bool checked among list<ItemInformationParent>?
        if (ItemHandle.GetEquippedItem() != null)
        {
            // Does said itemInformationParent have the same name as passed interact() enum variable?
            if (ItemName_.ToString() == ItemHandle.GetEquippedItem().ItemEnumName)
            {
                switch (ItemName_)
                {
                    case Interactable.ItemName.Axe:
                        DoAxe();
                        break;
                    case Interactable.ItemName.Sword:
                        DoSword();
                        break;
                    default:
                        DoNone();
                        break;
                }
            }
            else
            {
                Debug.Log("Wrong object equipped, Name isn't a match");
                Debug.Log("Current Item Equipped " + ItemHandle.GetEquippedItem().ItemEnumName);
                Debug.Log("Current Item required " + ItemName_.ToString());
                //Debug.Log("Is your Interact() enum correct? ItemInformationChild Line 10, Interactable Line 7, Do these enums match? exact wording is key)");
            }
        }
    }
    public void ScheduleInteraction(Interactable.ItemName ItemName_)
    {
        InteractTools(MyItemHandler, ItemName_);
    }

    // Start is called before the first frame update
    void Start()
    {
        MyItemHandler = ItemHandler.Instance;
        StartCoroutine(TestAxe());
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TestAxe() 
    {
        yield return new WaitForSeconds(3);
        ScheduleInteraction(Interactable.ItemName.Axe);
    }

    #region Action Voids
    public void DoNone()
    {
        // Add your code here....
        Debug.Log("None");
    }
    public void DoAxe()
    {
        // Add your code here....
        Debug.Log("Axe");
    }
    public void DoSword()
    {
        // Add your code here....
        Debug.Log("Sword");
    }

    #endregion
}
