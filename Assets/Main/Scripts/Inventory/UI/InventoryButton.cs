using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // external API for other scripts to open the inventory if it's currently closed
    public void OpenIfClosed()
    {
        if (inventoryPanel == null)
        {
            Debug.LogWarning("InventoryButton: inventoryPanel Î´°ó¶¨¡£");
            return;
        }

        if (!inventoryPanel.activeSelf)
            inventoryPanel.SetActive(true);
    }
}
