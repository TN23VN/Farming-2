using System.Collections.Generic;
using PolyAndCode.UI;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;

public class RecyclableInventoryManager : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;
    [SerializeField]
    private int _dataLength;

    public GameObject inventoryGO;
    //Dummy data List
    private List<InventoryItems> _inventoryItems = new List<InventoryItems>();

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    } 
    
    public int GetItemCount()
    {
        return _inventoryItems.Count;
    }
    
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as CellItemData;
        item.ConfigureCell(_inventoryItems[index], index);
    }

    private void Start()
    {
        List<InventoryItems> listItem = new List<InventoryItems>();
        for(int i=0;i<10;i++)
        {
            InventoryItems item = new InventoryItems();
            item.name = "Name_"+i.ToString();
            item.description = "Des_"+i.ToString();

            listItem.Add(item);
        }
        SetListItem(listItem);
        _recyclableScrollRect.ReloadData();
    }
    public void SetListItem(List<InventoryItems> list)
    {
        _inventoryItems = list;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InventoryItems itemDemo = new InventoryItems("Demo","Demo");
            _inventoryItems.Add(itemDemo);
            _recyclableScrollRect.ReloadData();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //inventoryGO.SetActive(!inventoryGO.activeSelf);
            Vector3 crrPosInven = inventoryGO.GetComponent<RectTransform>().anchoredPosition;
            inventoryGO.GetComponent<RectTransform>().anchoredPosition = crrPosInven.y == 1000 ? Vector3.zero : new Vector3(0, 1000, 0);
            
        }
    }

    public void AddInventoryItem(InventoryItems item)
    {
        _inventoryItems.Add(item);
        _recyclableScrollRect.ReloadData();
    }
}
