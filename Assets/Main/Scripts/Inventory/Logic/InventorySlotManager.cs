using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotManager  : Singleton<InventorySlotManager>
{
    //old
    //public Image[] slots;

    public InventoryButton inventoryButton;


    //public void AddItem(Sprite itemSprite)
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (!slots[i].enabled)
    //        {
    //            slots[i].sprite = itemSprite;
    //            slots[i].enabled = true;
    //            slots[i].preserveAspect = true;

    //            // open inventory panel if it's closed
    //            if (inventoryButton != null)
    //            {
    //                inventoryButton.OpenIfClosed();
    //            }
    //            else
    //            {
    //                Debug.LogWarning("InventorySlotManager: inventoryButton not found");
    //            }

    //            return;
    //        }
    //    }

    //    Debug.Log("Inventory Full");
    //}

    //public void RemoveItem(int index)
    //{
    //    slots[index].enabled = false;
    //}

    // new 暂时缺少删除物件信息功能
    public SlotUI[] slotUIs;
    public int currentIndex;
    private void OnEnable() {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }
    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }
    private void OnUpdateUIEvent(ItemDetails itemdetails, int index)
    {
        if (itemdetails == null)
        {
            slotUIs[index].SetEmpty();
            currentIndex = -1;//?
        }
        else
        {
            currentIndex = index;
            slotUIs[index].SetItem(itemdetails);
            // open inventory panel if it's closed
            if (inventoryButton != null)
            {
                inventoryButton.OpenIfClosed();
            }
            else
            {
                Debug.LogWarning("InventorySlotManager: inventoryButton not found");
            }
        }
    }

}
