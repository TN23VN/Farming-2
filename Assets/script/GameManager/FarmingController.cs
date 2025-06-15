using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingController : MonoBehaviour
{
    public Animator animation;
    public Notification myNotification;
    public InventoryManager inventoryManager;

    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;

    public TileBase tb_Ground;
    public TileBase tb_Grass;
    public TileBase tb_Forest;

    public TileMapManager tileMapManager;
    private void Start()
    {
        
    }

    void Update()
    {
        HandleFarmAction();
    }

    public void HandleFarmAction()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animation.SetBool("DaoDat",true);
            Vector3Int cellPos=tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Grass.GetTile(cellPos);

            if (crrTileBase != null && crrTileBase.Equals(tb_Grass))
            {
                tm_Grass.SetTile(cellPos, null);
                tm_Ground.SetTile(cellPos, tb_Ground);
                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Ground);
            }
            else
            {
                myNotification.ShowMessage("Không thể đào ở đây!");
                
            }    
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animation.SetBool("TrongCay", true);
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Ground.GetTile(cellPos);

            if (crrTileBase != null && crrTileBase.Equals(tb_Ground))
            {
                // Lấy hạt giống đầu tiên trong inventory
                InventoryItem seedItem = inventoryManager.GetFirstSeedItem();
                if (seedItem != null)
                {
                    StartCoroutine(GrowPlant(cellPos, tm_Forest, seedItem.seedData));
                    tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Potato);
                    inventoryManager.RemoveItem(seedItem.itemName, 1);
                }
                else
                {
                    myNotification.ShowMessage("Bạn không có hạt giống!");
                }
            }
            else
            {
                myNotification.ShowMessage("Không thể trồng cây ở đây!");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Animation
            animation.SetBool("ThuHoach", true);
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Forest.GetTile(cellPos);
            SeedData plantedSeed = tileMapManager.GetPlantedSeed(cellPos.x, cellPos.y);
            if (plantedSeed != null && crrTileBase.Equals(plantedSeed.growthStages[^1])) // Giai đoạn cuối
            {
                tm_Forest.SetTile(cellPos, null);
                tm_Grass.SetTile(cellPos, tb_Grass);

                InventoryItem product = new InventoryItem(plantedSeed.productName, plantedSeed.icon, 1 , plantedSeed);
                inventoryManager.AddItem(product);

                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Grass);
                tileMapManager.ClearPlantedSeed(cellPos.x, cellPos.y);
            }
            else
            {
                myNotification.ShowMessage("Không thể thu hoạch!");
            }
        }
        if (Input.GetKeyUp(KeyCode.C)) 
        {
            animation.SetBool("DaoDat", false);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            animation.SetBool("TrongCay", false);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            animation.SetBool("ThuHoach", false);
        }
    }

    public IEnumerator GrowPlant(Vector3Int cellPos, Tilemap tilemap, SeedData seed)
    {
        int stage = 0;
        while (stage < seed.growthStages.Count)
        {
            tilemap.SetTile(cellPos, seed.growthStages[stage]);
            yield return new WaitForSeconds(10);
            stage++;
        }

        // Ghi lại seed tại vị trí này để dùng khi thu hoạch
        tileMapManager.SetPlantedSeed(cellPos.x, cellPos.y, seed);
    }
}
