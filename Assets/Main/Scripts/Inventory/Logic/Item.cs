using UnityEngine;
using System.Collections.Generic;

public class Item: MonoBehaviour
{
    public Sprite itemIcon;
    public string itemId;
    public ItemID itemID; //物件的唯一ID（与ItemSO中的ID对应）

    // old
    //void OnMouseDown()
    //{
    //    InventorySlotManager.Instance.AddItem(itemIcon);

    //    // 标记为已拾取（用于跨场景保留）
    //    if (GameManager.Instance != null)
    //    {
    //        GameManager.Instance.MarkItemPicked(itemId);
    //    }

    //    Destroy(gameObject);
    //}

    // new（暂时缺少跨场景的状态管理）
    public void ItemClicked() {
        InventoryController.Instance.AddItem(itemID);
        this.gameObject.SetActive(false);
    }
}
