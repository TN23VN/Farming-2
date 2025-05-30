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

    private Map map;
    private FirebaseDatabaseManager databaseManager;
    private FirebaseUser user;
    private DatabaseReference reference;
    private void Start()
    {
        map = new Map();
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        //WriteAllTileMapToFirebase();
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadMapForUser();
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
        map = new Map(tilemaps);
        Debug.Log(map.ToString());

        databaseManager.WriteDatabase(user.UserId+ "/Map", map.ToString());
    }

    public void LoadMapForUser()
    {
        reference.Child("Users").Child(user.UserId + "/Map").GetValueAsync().ContinueWithOnMainThread(task=>
        {
            if (task.IsCanceled)
            {
                return;
            }
            else if (task.IsFaulted)
            {
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                map = JsonConvert.DeserializeObject<Map>(snapshot.Value.ToString());
                Debug.Log(map.ToString());
                MapToUI(map);
            }
        });
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
        for(int i = 0;i < map.GetLength();i++)
        {
            if (map.lstTileMapDetail[i].x == x && map.lstTileMapDetail[i].y == y)
            {
                map.lstTileMapDetail[i].tilemapState = state;
                databaseManager.WriteDatabase(user.UserId + "/Map", map.ToString());
                Debug.Log("Save to firebase!");
            }
        }
    }
}
