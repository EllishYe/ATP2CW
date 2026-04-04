using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class SlotUI : MonoBehaviour
{
    public Image itemImage;
    private ItemDetails currentItem;

    public void SetItem(ItemDetails itemdetails) {
        currentItem = itemdetails;
        itemImage.sprite = itemdetails.frontSprite;
        itemImage.enabled = true;
        itemImage.preserveAspect = true;
    }

    public void SetEmpty()
    {
        currentItem = null;
        itemImage.enabled = false;
    }

    #region Public Methods
    public ItemDetails GetItemDetails()
    {
        return currentItem;
    }
    #endregion
}
