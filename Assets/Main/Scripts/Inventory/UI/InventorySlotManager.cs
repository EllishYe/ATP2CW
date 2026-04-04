using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotManager  : Singleton<InventorySlotManager>
{
    public Image[] slots;

    public InventoryButton inventoryButton;

    public void AddItem(Sprite itemSprite)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].enabled)
            {
                slots[i].sprite = itemSprite;
                slots[i].enabled = true;
                slots[i].preserveAspect = true;

                // open inventory panel if it's closed
                if (inventoryButton != null)
                {
                    inventoryButton.OpenIfClosed();
                }
                else
                {
                    Debug.LogWarning("InventorySlotManager: inventoryButton not found");
                }

                return;
            }
        }

        Debug.Log("Inventory Full");
    }

    public void RemoveItem(int index)
    {
        slots[index].enabled = false;
    }


    //private ItemDetails currentItem;
    //public Image itemImage;
    //public void SetItem(ItemDetails itemDetails) {
    //    currentItem = itemDetails;
    //    //itemImage.sprite = itemDetails.frontSprite;
    //}

    
}
