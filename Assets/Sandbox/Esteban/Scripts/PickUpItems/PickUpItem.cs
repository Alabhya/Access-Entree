using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public Sprite ItemSprite;
    public float RotateSpeed = 0;
    private float ItemRotationSpeed;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ItemRotationSpeed += Time.deltaTime * RotateSpeed;
        this.transform.transform.rotation = Quaternion.Euler(0, ItemRotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ToolBarUI.Instance.AddItem(ItemSprite);
            //Inventory.add(item.name,item.quantity);
            Destroy(this.gameObject);
        }
    }

}
