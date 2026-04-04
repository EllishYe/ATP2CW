using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }
    //items
    private Dictionary<ItemID, bool> itemAvailableDict = new Dictionary<ItemID, bool>();
    //puzzle items
    private HashSet<string> collectedPuzzleItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent; 
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        // Check items before unloading the scene
        foreach (var item in Object.FindObjectsByType<Item>(FindObjectsSortMode.None))
        {
            if (!itemAvailableDict.ContainsKey(item.itemID))
            {
                //Mark the item as available for the next scene
                itemAvailableDict[item.itemID] = true;
            }
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        // Check items before loading the scene
        foreach (var item in Object.FindObjectsByType<Item>(FindObjectsSortMode.None))
        {
            if (!itemAvailableDict.ContainsKey(item.itemID))
                itemAvailableDict[item.itemID] = true;
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemID]);
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int arg2)
    {
        if (itemDetails != null) {
            itemAvailableDict[itemDetails.itemID] = false;//Mark the item as picked up
        }
    }

    #region External API: items and puzzle items
    public bool IsItemAvailable(ItemID id)
    {
        if (!itemAvailableDict.TryGetValue(id, out var available))
            return true;
        return available;
    }

    public void MarkPuzzleItemCollected(string persistentId)
    {
        if (string.IsNullOrEmpty(persistentId)) return;
        collectedPuzzleItems.Add(persistentId);
    }

    public bool IsPuzzleItemCollected(string persistentId)
    {
        if (string.IsNullOrEmpty(persistentId)) return false;
        return collectedPuzzleItems.Contains(persistentId);
    }
    #endregion
}
