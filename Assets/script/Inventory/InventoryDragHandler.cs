using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public InventorySlot slot;

    private Image dragImage;
    private Transform canvasTransform;
    private InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragImage = GameObject.Find("DragItemImage").GetComponent<Image>();
        canvasTransform = GameObject.Find("Canvas").transform;
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.GetItem() != null)
        {
            dragImage.sprite = slot.GetItem().icon;
            dragImage.color = Color.white;
            dragImage.enabled = true;
            dragImage.transform.position = Input.mousePosition;

            inventoryManager.draggingItem = slot.GetItem();
            inventoryManager.sourceSlot = slot;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragImage.enabled)
            dragImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragImage.enabled = false;

        // Check xem có thả vào slot khác không
        GameObject targetObj = eventData.pointerCurrentRaycast.gameObject;
        if (targetObj != null)
        {
            InventorySlot targetSlot = targetObj.GetComponentInParent<InventorySlot>();
            if (targetSlot != null && targetSlot != inventoryManager.sourceSlot)
            {
                inventoryManager.SwapItems(inventoryManager.sourceSlot, targetSlot);
                return;
            }
        }
    }
}
