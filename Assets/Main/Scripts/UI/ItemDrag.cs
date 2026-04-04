using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 物品拖拽组件，允许物品被拖动到DropZone中以触发交互
/// </summary>
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPos;
    Transform startParent;
    Canvas canvas;

    bool isDropped = false;
    SlotUI slotUI;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        slotUI = GetComponentInParent<SlotUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = transform.position;
        startParent = transform.parent;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.position = startPos;
        //transform.SetParent(startParent);

        if (!isDropped)
        {
            // Not in drop zone -> return to original position
            transform.position = startPos;
            transform.SetParent(startParent);
        }
        else
        {
            // in drop zone -> disable the item image
            if (slotUI != null)
            {
                slotUI.itemImage.enabled = false;
                transform.position = startPos;
                transform.SetParent(startParent);
            }
        }
    }

    #region Public Mthods
    public void SetDropped()
    {
        isDropped = true;
    }
    public ItemDetails GetItemDetails()
    {
        return slotUI != null ? slotUI.GetItemDetails() : null;
    }
    #endregion
}
