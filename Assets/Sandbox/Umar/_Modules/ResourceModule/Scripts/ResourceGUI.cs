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
        itemName.text = inventoryItem.itemName;
        itemAmount.text = inventoryItem.itemAmount.ToString();
        itemImg.sprite = inventoryItem.itemImg;
    }
}
