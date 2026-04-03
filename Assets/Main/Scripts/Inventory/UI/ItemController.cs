using UnityEngine;
using System.Collections.Generic;

public class ItemController : MonoBehaviour
{
    public Sprite itemIcon;
    public string itemId;
    public ItemID itemID;

    void OnMouseDown()
    {
        InventorySlotManager.Instance.AddItem(itemIcon);

        // 标记为已拾取（用于跨场景保留）
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MarkItemPicked(itemId);
        }

        Destroy(gameObject);
    }

    // new
    public void ItemClicked() {
        InventoryController.Instance.AddItem(itemID);
        this.gameObject.SetActive(false);
    }
}
