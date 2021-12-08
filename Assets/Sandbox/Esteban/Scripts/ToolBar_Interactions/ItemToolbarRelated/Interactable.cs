using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    public enum ItemName { None, Axe, Sword};
    void InteractTools(ItemHandler ItemHandle, ItemName ItemName_);
}
