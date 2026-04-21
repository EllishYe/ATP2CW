using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySceneVisibilityController : MonoBehaviour
{
    public GameObject inventoryButtonObject;
    public GameObject inventoryPanelObject;
    public string[] hideInScenes;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void Start()
    {
        UpdateInventoryVisibility();
    }

    private void OnAfterSceneLoadedEvent()
    {
        UpdateInventoryVisibility();
    }

    private void UpdateInventoryVisibility()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        bool shouldHide = false;

        for (int i = 0; i < hideInScenes.Length; i++)
        {
            if (currentSceneName == hideInScenes[i])
            {
                shouldHide = true;
                break;
            }
        }

        if (inventoryButtonObject != null)
            inventoryButtonObject.SetActive(!shouldHide);

        if (inventoryPanelObject != null)
            inventoryPanelObject.SetActive(false);
    }
}