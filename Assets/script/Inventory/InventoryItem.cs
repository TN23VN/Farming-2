using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public SeedData seedData; // null nếu không phải hạt giống

    public InventoryItem(string name, Sprite icon, int quantity, SeedData seed = null)
    {
        this.itemName = name;
        this.icon = icon;
        this.quantity = quantity;
        this.seedData = seed;
    }
    public InventoryItem()
    {
    }
    public bool IsSeed()
    {
        return seedData != null;
    }
}
