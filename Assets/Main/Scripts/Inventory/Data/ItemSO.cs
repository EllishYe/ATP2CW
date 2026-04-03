using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemOS")]
public class ItemSO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;
    public ItemDetails GetItemDetails(ItemID itemID)
    {
        return itemDetailsList.Find(x => x.itemID == itemID);
    }

    [System.Serializable]
    public class ItemDetails
    {
        [Header("ID")]
        public ItemID itemID;

        [Header("UI")]
        public Sprite inventoryIcon;
        public Sprite frontSprite;
        public Sprite backSprite;
    }
}
