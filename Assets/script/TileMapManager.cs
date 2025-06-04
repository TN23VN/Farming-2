using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tm_Grass;
    public Tilemap tm_Ground;
    public Tilemap tm_Forest;
    public TileBase tb_Forest;

    private FirebaseDatabaseManager databaseManager;
    private DatabaseReference reference;
    private void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        if (LoadDataManager.userInGame.MapInGame.lstTileMapDetail == null)
        {
            WriteAllTileMapToFirebase();
        }
        else
        {
            LoadMapForUser();
        }
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
    }

    public void WriteAllTileMapToFirebase()
    {
        List<TilemapDetail> tilemaps = new List<TilemapDetail>();
        for (int x = tm_Grass.cellBounds.min.x ; x < tm_Grass.cellBounds.max.x ; x++)
        {
            for (int y = tm_Grass.cellBounds.min.y; y < tm_Grass.cellBounds.max.y; y++)
            {
                TilemapDetail tm_detail = new TilemapDetail(x,y,TilemapState.Grass);
                tilemaps.Add(tm_detail);
            }
        }
        LoadDataManager.userInGame.MapInGame = new Map(tilemaps);
        databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
    }

    public void LoadMapForUser()
    {
        MapToUI(LoadDataManager.userInGame.MapInGame);
    }
    public void TileMapDetailToTileBase(TilemapDetail tilemapDetail)
    {
        Vector3Int cellPos = new Vector3Int(tilemapDetail.x, tilemapDetail.y,0);
        if (tilemapDetail.tilemapState == TilemapState.Ground)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Forest.SetTile(cellPos, null);
        }
        else if(tilemapDetail.tilemapState == TilemapState.Grass)
        {
            tm_Forest.SetTile(cellPos, null);
        }
        else if (tilemapDetail.tilemapState == TilemapState.Forest)
        {
            tm_Grass.SetTile(cellPos, null);
            tm_Forest.SetTile(cellPos,tb_Forest);
        }
    }

    public void MapToUI(Map map)
    {
        for (int i = 0; i < map.GetLength(); i++)
        {
            TileMapDetailToTileBase(map.lstTileMapDetail[i]);
        }
    }

    public void SetStateForTilemapDetail(int x, int y, TilemapState state)
    {
        for(int i = 0;i < LoadDataManager.userInGame.MapInGame.GetLength();i++)
        {
            if (LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].x == x && LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].y == y)
            {
                LoadDataManager.userInGame.MapInGame.lstTileMapDetail[i].tilemapState = state;
                databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
                Debug.Log("Save to firebase!");
            }
        }
    }
}
