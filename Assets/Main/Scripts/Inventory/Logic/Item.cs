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
        
        if (FireplaceEndingManager.Instance != null)
        {
            //Debug.Log("Calling CheckBackpackProgress from ItemClicked");
            FireplaceEndingManager.Instance.CheckBackpackProgress();
        }
        else
        {
            //Debug.Log("FireplaceEndingManager.Instance is NULL in ItemClicked");
        }

        clickableItem.OnClick();
        this.gameObject.SetActive(false);
    }
}
