using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Sprite itemIcon;
    public string itemId;

    void OnMouseDown()
    {
        InventorySlotManager.Instance.AddItem(itemIcon);

        // 标记为已拾取（用于跨场景保留）
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MarkItemPicked(itemId);
        }

        Destroy(gameObject);
    }
}
