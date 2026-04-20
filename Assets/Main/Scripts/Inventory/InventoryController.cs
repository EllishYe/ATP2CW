using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class InventoryController : Singleton<InventoryController>
    
{
    public ItemSO itemData;
    [SerializeField] private List<ItemID> itemList = new List<ItemID>();

    public void AddItem(ItemID itemID)
    {
        if (!itemList.Contains(itemID))
        {
            itemList.Add(itemID);
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemID), itemList.Count - 1);
        }
    }
    public bool RemoveItem(ItemID itemID)
    {
        int idx = itemList.IndexOf(itemID);
        if (idx < 0) return false;

        // Remove data
        itemList.RemoveAt(idx);

        // update UI 
        for (int i = idx; i < itemList.Count; i++)
        {
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
        }

        // delete the last one
        EventHandler.CallUpdateUIEvent(null, itemList.Count);

        return true;
    }
    
    public int GetItemCount()
    {
        return itemList.Count;
    }

    public bool IsEmpty()
    {
        return itemList.Count == 0;
    }
}
