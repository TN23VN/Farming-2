using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingController : MonoBehaviour
{
    public Animator animation;

    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;

    public TileBase tb_Ground;
    public TileBase tb_Grass;
    public TileBase tb_Forest;

    public List<TileBase> lst_Potato;
    //public List<TileBase> lst_ThuHoach;

    private RecyclableInventoryManager recyclableInventoryManager;

    public TileMapManager tileMapManager;
    private void Start()
    {
        
        recyclableInventoryManager = GameObject.Find("InventoryManager").GetComponent<RecyclableInventoryManager>();
        animation = GetComponent<Animator>();
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
            if (crrTileBase = tb_Grass)
            {
                tm_Grass.SetTile(cellPos, null);
                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Ground);
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            animation.SetBool("TrongCay", true);
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Grass.GetTile(cellPos);
            if (crrTileBase == null)
            {
                StartCoroutine(GrowPlant(cellPos , tm_Forest, lst_Potato));
                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Potato);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            animation.SetBool("ThuHoach", true);
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Forest.GetTile(cellPos);
            if (crrTileBase = lst_Potato[4])
            {
                //set lai phan dat
                tm_Grass.SetTile(cellPos, tb_Grass);
                //thu hoach hoa
                tm_Forest.SetTile(cellPos, null);
                //them vao tui do
                InventoryItems itemPotato = new InventoryItems();
                itemPotato.name = "Khoai tay";
                itemPotato.description = "Cu khoai tay";

                recyclableInventoryManager.AddInventoryItem(itemPotato);
                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Grass);
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

    public IEnumerator GrowPlant(Vector3Int cellPos, Tilemap tilemap, List<TileBase> lstTileBase)
    {
        int crrStage = 0;
        while (crrStage < lstTileBase.Count)
        {
            tilemap.SetTile(cellPos, lstTileBase[crrStage]);
            yield return new WaitForSeconds(10);
            crrStage++;
        }
    }
}
