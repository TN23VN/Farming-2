using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewSeedData", menuName = "Farming/Seed Data", order = 1)]
public class SeedData : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public List<TileBase> growthStages;
    public float growthTimePerStage = 10f; // ⏳ mỗi giai đoạn bao lâu
    public string productName;
    public Sprite productIcon;
    public int minYield = 1; // Sản lượng thu hoạch thấp nhất
    public int maxYield = 3; // Sản lượng thu hoạch cao nhất
}
