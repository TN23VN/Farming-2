using System;
using Newtonsoft.Json;
using UnityEngine;


public enum TilemapState
{
    Ground,
    Grass,
    Forest,
    Potato,
    Grape
}
public class TilemapDetail
{
    public int x { get; set; }
    public int y { get; set; }
    public TilemapState tilemapState { get; set; }

    public DateTime ThoiGian { get; set; }

    public TilemapDetail()
    {

    }

    public TilemapDetail(int x, int y, TilemapState tilemapState , DateTime tgian)
    {
        this.x = x;
        this.y = y;
        this.tilemapState = tilemapState;
        this.ThoiGian = tgian;
    }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
