using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopItemUIPrefab;
    public Transform contentPanel;
    public List<ShopItemData> availableItems;
    public InventoryManager inventoryManager;

    void Start()
    {
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

            SaveUserToFirebase();
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

            SaveUserToFirebase();
        }
    }

    private void SaveUserToFirebase()
    {
        string userId = LoadDataManager.firebaseUser.UserId;
        string json = LoadDataManager.userInGame.ToString();
        FirebaseDatabase.DefaultInstance
            .RootReference
            .Child("Users")
            .Child(userId)
            .SetRawJsonValueAsync(json);
    }
}
