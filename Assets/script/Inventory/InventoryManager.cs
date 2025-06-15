using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public InventoryItem draggingItem;
    public InventorySlot sourceSlot;
    public GameObject inventoryUI;

    private List<InventoryItem> items = new List<InventoryItem>();

    void Start()
    {
        InventoryItem testItem = new InventoryItem("Khoai tây", null, 3);
        AddItem(testItem);
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
}
