using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public InventoryItem draggingItem;
    public InventorySlot sourceSlot;
    public GameObject inventoryUI;

    public SeedData potatoSeedData;
    public SeedData tomatoSeedData;

    private List<InventoryItem> items = new List<InventoryItem>();

    void Start()
    {
        AddItem(new InventoryItem("Khoai tây", potatoSeedData.icon, 1, potatoSeedData));
        AddItem(new InventoryItem("Cà chua", tomatoSeedData.icon, 3, tomatoSeedData));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    public void AddItem(InventoryItem newItem)
    {
        // Tìm item đã có
        foreach (var slot in slots)
        {
            InventoryItem current = slot.GetItem();
            if (current != null && current.itemName == newItem.itemName)
            {
                current.quantity += newItem.quantity;
                slot.SetItem(current);
                return;
            }
        }

        // Tìm slot trống
        foreach (var slot in slots)
        {
            if (slot.GetItem() == null)
            {
                slot.SetItem(newItem);
                return;
            }
        }

        Debug.Log("Inventory full!");
    }

    public void SwapItems(InventorySlot a, InventorySlot b)
    {
        InventoryItem temp = a.GetItem();
        a.SetItem(b.GetItem());
        b.SetItem(temp);
    }

    public InventoryItem GetFirstSeedItem()
    {
        foreach (var slot in slots)
        {
            var item = slot.GetItem();
            if (item != null && item.IsSeed())
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveItem(string itemName, int count)
    {
        foreach (var slot in slots)
        {
            var item = slot.GetItem();
            if (item != null && item.itemName == itemName)
            {
                item.quantity -= count;
                if (item.quantity <= 0)
                    slot.ClearSlot();
                else
                    slot.SetItem(item);
                return;
            }
        }
    }
}
