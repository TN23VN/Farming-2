using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewSeedData", menuName = "Farming/Seed Data")]
public class SeedData : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public List<TileBase> growthStages; // 0 → n (sprites cho các giai đoạn phát triển)
    public TileBase harvestTile;        // dùng để kiểm tra khi thu hoạch
    public string productName;          // Tên sản phẩm thu hoạch
}
