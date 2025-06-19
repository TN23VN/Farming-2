using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int buyPrice;
    public int sellPrice;
    public bool isSeed;
    public SeedData seedData; // nếu là hạt giống
}
