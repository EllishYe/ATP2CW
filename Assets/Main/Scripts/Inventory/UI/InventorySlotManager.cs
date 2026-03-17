using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotManager  : Singleton<InventorySlotManager>
{
    public Image[] slots;

    

    public void AddItem(Sprite itemSprite)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].enabled)
            {
                slots[i].sprite = itemSprite;
                slots[i].enabled = true;
                return;
            }
        }

        Debug.Log("Inventory Full");
    }

    public void RemoveItem(int index)
    {
        slots[index].enabled = false;
    }
}
