using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
