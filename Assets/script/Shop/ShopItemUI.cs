using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text priceText;
    public Button buyButton;
    public Button sellButton;

    private ShopItemData data;
    private ShopManager manager;

    public void Setup(ShopItemData itemData, ShopManager shopManager)
    {
        data = itemData;
        manager = shopManager;

        icon.sprite = itemData.icon;
        nameText.text = itemData.itemName;
        priceText.text = $"Mua: {itemData.buyPrice} / Bán: {itemData.sellPrice}";

        buyButton.onClick.AddListener(() => manager.Buy(data));
        sellButton.onClick.AddListener(() => manager.Sell(data));
    }
}
