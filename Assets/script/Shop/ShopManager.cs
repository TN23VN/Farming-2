using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopItemUIPrefab;
    public Transform contentPanel;
    public List<ShopItemData> availableItems;
    public Text txtGold;
    public Text txtDiamond;


    public InventoryManager inventoryManager;
    private FirebaseDatabaseManager databaseManager;

    void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        PopulateShop();
    }

    public void PopulateShop()
    {
        foreach (var itemData in availableItems)
        {
            GameObject itemUI = Instantiate(shopItemUIPrefab, contentPanel);
            itemUI.GetComponent<ShopItemUI>().Setup(itemData, this);
        }
    }

    public void Buy(ShopItemData data)
    {
        if (LoadDataManager.userInGame.Gold >= data.buyPrice)
        {
            LoadDataManager.userInGame.Gold -= data.buyPrice;

            InventoryItem item = data.isSeed
                ? new InventoryItem(data.itemName, data.icon, 1, data.seedData)
                : new InventoryItem(data.itemName, data.icon, 1);

            inventoryManager.AddItem(item);
            
            databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
            txtGold.text = "Gold: " + LoadDataManager.userInGame.Gold.ToString();
            txtDiamond.text = "Diamond: " + LoadDataManager.userInGame.Diamond.ToString();
        }
        else
        {
            Debug.Log("Không đủ tiền!");
        }
    }

    public void Sell(ShopItemData data)
    {
        if (inventoryManager.HasItem(data.itemName))
        {
            inventoryManager.RemoveItem(data.itemName, 1);
            LoadDataManager.userInGame.Gold += data.sellPrice;
            databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
            txtGold.text = "Gold: " + LoadDataManager.userInGame.Gold.ToString();
            txtDiamond.text = "Diamond: " + LoadDataManager.userInGame.Diamond.ToString();
        }
    }
}
