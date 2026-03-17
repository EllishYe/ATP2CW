using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Sprite itemIcon;
    

    void OnMouseDown()
    {
        InventorySlotManager.Instance.AddItem(itemIcon);
        Destroy(gameObject);
    }
}
