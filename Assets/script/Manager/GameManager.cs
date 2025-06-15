using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;                   // Tilemap gốc
    public Transform characterTransform;      // Vị trí nhân vật
    public Vector2 facingDirection = Vector2.up; // Hướng nhìn nhân vật
    public GameObject highlightPrefab;        // Prefab để đánh dấu tile

    private GameObject currentHighlight;      // Thể hiện vùng đang được đánh dấu

    void Start()
    {
        
    }

    void Update()
    {
        //Xác định ô tile phía trước nhân vật
        Vector3Int currentCell = tilemap.WorldToCell(characterTransform.position);
        Vector3Int targetCell = currentCell + new Vector3Int((int)facingDirection.x, (int)facingDirection.y, 0);

        //Lấy vị trí tâm của ô đó
        Vector3 worldPos = tilemap.GetCellCenterWorld(targetCell);

        //Hiển thị or di chuyển highlight
        if (currentHighlight == null)
        {
            currentHighlight = Instantiate(highlightPrefab, worldPos, Quaternion.identity);
        }
        else
        {
            currentHighlight.transform.position = worldPos;
        }
    }
}
