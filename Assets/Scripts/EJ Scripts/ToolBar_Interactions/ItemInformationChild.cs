using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemInformationChild : MonoBehaviour
{
    public Image MyImage;

    public enum ItemName {None, Axe, Sword};


    public ItemName ItemNames;
    // Start is called before the first frame update
    void Start()
    {
        MyImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
