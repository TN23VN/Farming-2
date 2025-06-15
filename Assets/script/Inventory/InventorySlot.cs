using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text countText;

    private InventoryItem currentItem;

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            countText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        }
        else
        {
            icon.enabled = false;
            countText.text = "";
        }
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }

    public void ClearSlot()
    {
        SetItem(null);
    }
}
