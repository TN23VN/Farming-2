using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

public class CellItemData : MonoBehaviour , ICell
{
    //UI
    public Text nameLabel;
    public Text desLabel;
    //Model
    private InventoryItems _contactInfo;
    private int _cellIndex;
    //This is called from the SetCell method in DataSource
    public void ConfigureCell(InventoryItems inventoryItems, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = inventoryItems;
        nameLabel.text = inventoryItems.name;
        desLabel.text = inventoryItems.description;
    }
}
