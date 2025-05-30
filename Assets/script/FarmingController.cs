using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingController : MonoBehaviour
{
    public Tilemap tm_Ground;
    public Tilemap tm_Grass;
    public Tilemap tm_Forest;

    public TileBase tb_Ground;
    public TileBase tb_Grass;
    public TileBase tb_Forest;

    private RecyclableInventoryManager recyclableInventoryManager;

    public TileMapManager tileMapManager;
    private void Start()
    {
        recyclableInventoryManager = GameObject.Find("InventoryManager").GetComponent<RecyclableInventoryManager>();
    }

    void Update()
    {
        HandleFarmAction();
    }

    public void HandleFarmAction()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
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
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Grass.GetTile(cellPos);
            if (crrTileBase == null)
            {
                tm_Grass.SetTile(cellPos, tb_Forest);
                tileMapManager.SetStateForTilemapDetail(cellPos.x, cellPos.y, TilemapState.Forest);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vector3Int cellPos = tm_Ground.WorldToCell(transform.position);
            TileBase crrTileBase = tm_Forest.GetTile(cellPos);
            if (crrTileBase = tb_Forest)
            {
                //set lai phan dat
                tm_Grass.SetTile(cellPos, tb_Grass);
                //thu hoach hoa
                tm_Forest.SetTile(cellPos, null);
                //them vao tui do
                InventoryItems itemFlower = new InventoryItems();
                itemFlower.name = "Bong hoa";
                itemFlower.description = "Mot bong hoa xinh dep";
                Debug.Log(itemFlower.ToString());
                recyclableInventoryManager.AddInventoryItem(itemFlower);
            }
        }
    }
}
