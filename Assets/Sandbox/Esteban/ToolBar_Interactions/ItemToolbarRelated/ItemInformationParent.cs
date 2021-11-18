using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationParent : MonoBehaviour
{
    public ItemInformationChild ItemReference;
    [Space]
    public string ItemEnumName;
    public bool IsEquipped;
    private Image MyImage;

    // Start is called before the first frame update
    void Start()
    {
        MyImage = this.GetComponent<Image>();
        SetItemValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Whenever a item is to be added to the toolbar, the item is a instantiated as a child of this scripts gameobject
    // this SetItemsValue() gets the values of its child and sets its values equal to the childs
    public void SetItemValues()
    {
        if(this.transform.childCount == 1)
        {
            this.ItemReference = this.transform.GetChild(0).GetComponent<ItemInformationChild>();
            #region Sets this UI image equal to item image
            if (ItemReference.MyImage.sprite != null)
            {
                MyImage.sprite = ItemReference.MyImage.sprite;
                
            } else
            {
                Debug.Log("This Object Does Not Have Image");
            }
            #endregion
            #region Sets item name equal to item enum
                ItemEnumName = ItemReference.ItemNames.ToString();
            #endregion
        }
    }

}
