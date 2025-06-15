using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public int quantity;

    public InventoryItem(string name, Sprite icon, int quantity)
    {
        this.itemName = name;
        this.icon = icon;
        this.quantity = quantity;
    }

    public InventoryItem()
    {
    }
}
