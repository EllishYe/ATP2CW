using UnityEngine;
using System.Collections.Generic;

public class Item: MonoBehaviour
{
    public Sprite itemIcon;
    public string itemId;
    public ItemID itemID; //物件的唯一ID（与ItemSO中的ID对应）

    public ClickableItem clickableItem;

    // new
    public void ItemClicked() {
        InventoryController.Instance.AddItem(itemID);
        clickableItem.OnClick();
        this.gameObject.SetActive(false);
    }
}
