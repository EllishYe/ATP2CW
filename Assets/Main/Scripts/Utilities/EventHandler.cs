using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }


    #region Inspectry Events
    public static event Action ScratchClearedEvent;
    public static void CallScratchClearedEvent()
    {
        ScratchClearedEvent?.Invoke();
    }

    public static event Action ConfirmFinishedEvent;
    public static void CallConfirmFinishedEvent()
    {
        ConfirmFinishedEvent?.Invoke();
    }

    #endregion
}
