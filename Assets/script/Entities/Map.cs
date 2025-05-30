using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

public class Map
{
    public List<TilemapDetail> lstTileMapDetail {  get; set; }

    public Map()
    {
    }

    public Map(List<TilemapDetail> lstTileMapDetail)
    {
        this.lstTileMapDetail = lstTileMapDetail;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int GetLength()
    {
        return lstTileMapDetail.Count;
    }
}
