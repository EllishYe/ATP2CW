using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotManager  : Singleton<InventorySlotManager>
{
    public InventoryButton inventoryButton;

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
