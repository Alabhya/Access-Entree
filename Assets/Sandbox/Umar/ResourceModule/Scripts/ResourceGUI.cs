using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class ResourceGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemAmount;
    [SerializeField] private Image itemImg;

    public void SetUpResourceUI(InventoryItem inventoryItem) {
        itemName.text = inventoryItem.itemObj.itemName;
        itemAmount.text = inventoryItem.itemObj.itemAmount.ToString();
        itemImg.sprite = inventoryItem.itemObj.itemImg;
    }
}
