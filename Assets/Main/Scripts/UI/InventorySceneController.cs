using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySceneController : MonoBehaviour
{
    [Header("Default Close InventoryUI Scene")]
    public GameObject inventoryUI;
    public string hideInSceneName = "SceneA";

    void Start()
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning("InventorySceneController: inventoryUI 未绑定");
            return;
        }

        ApplyState(SceneManager.GetActiveScene());
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyState(scene);
    }

    void ApplyState(Scene scene)
    {
        if (inventoryUI == null) return;

        if (scene.name == hideInSceneName)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }
}
