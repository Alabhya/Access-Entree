using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationParent : MonoBehaviour
{
    public ItemInformationChild ItemReference;
    private Image MyImage;
    [Space]
    public string ItemEnumName;
    public bool IsEquipped;

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
                Debug.LogError("This Object Does Not Have Image");
            }
            #endregion
            #region Sets item name equal to item enum
                ItemEnumName = ItemReference.ItemNames.ToString();
            #endregion
        }
    }

    public void ResetValues()
    {
        MyImage.sprite = null;
        ItemEnumName = "";
    }

}
