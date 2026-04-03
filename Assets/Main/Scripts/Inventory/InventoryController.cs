using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class InventoryController : Singleton<InventoryController>
{
    [SerializeField] private List<ItemID> itemList = new List<ItemID>();

    public void AddItem(ItemID itemID)
    {
        if (!itemList.Contains(itemID))
        {
            itemList.Add(itemID);
            //TODO:¸üÐÂUIÏÔÊ¾
        }
    }

}
