using ScratchCardAsset;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectManager : MonoBehaviour
{

    private ItemDetails currentItem;

    public DropZone dropzone;// Use Canvas
    public HoldButtonController confirmCanvas;// Confirm Canvas
    public ScratchCardCanvasController scratchCanvas;// Scratch Canvas
    public ScratchCardManager scratchManager; // Scratch Manager
    public DissolveController dissolveController;// Dissolve Controller
    public FireInputTest fireController;// Fire Contoller
    
    [Header("Audio")]
    public ClickableItem scratchShowSound;



    #region Event Subscription
    void OnEnable()
    {
        EventHandler.ScratchClearedEvent += OnScratchCleared_Event;
        EventHandler.ConfirmFinishedEvent += OnConfirmFinished_Event;
        EventHandler.DissolveFinishedEvent += OnDissolveFinished_Event;
    }
    void OnDisable()
    {
        EventHandler.ScratchClearedEvent -= OnScratchCleared_Event;
        EventHandler.ConfirmFinishedEvent -= OnConfirmFinished_Event;
        EventHandler.DissolveFinishedEvent -= OnDissolveFinished_Event;
    }
    void OnScratchCleared_Event()
    {
        OnScratchFinished();
    }
    void OnConfirmFinished_Event()
    {
        OnConfirmFinished();
    }

    void OnDissolveFinished_Event()
    {
        OnDissolveFinished();
    }
    #endregion


    public void StartInspect(ItemDetails item)
    {
        currentItem = item;

        if (scratchManager != null)
        {
            scratchManager.ScratchSurfaceSprite = item.frontSprite;
            scratchManager.gameObject.SetActive(true);
        }

        scratchCanvas.ReceiveItem(item);
        scratchCanvas.SetInteractable(false);

        scratchCanvas.Show(scratchCanvas.frontImage);
        scratchCanvas.Show(scratchCanvas.backImage);
    }

    public void OnScratchFinished()
    {
        //reset scratch manager's Erase Progress of front image
        if (scratchManager != null) {
            scratchManager.ClearScratchCard();
            scratchManager.gameObject.SetActive(false);
        }
        confirmCanvas.ShowHoldButton();
        scratchShowSound.OnClick();
    }

    public void OnConfirmFinished()
    {
        Debug.Log("Confirm Finished");

        // Remove the item form inventory data and update UI
        if (currentItem != null && InventoryController.Instance != null)
        {
            InventoryController.Instance.RemoveItem(currentItem.itemID);
            currentItem = null;
        }

        dissolveController.PlayDissolve(); 

        //LockInput(true); 

    }

    public void OnDissolveFinished()
    {
        //LockInput(false);
        fireController.TriggerBurn();
        Debug.Log("Inspect Finished");
    }

    /*
    public void LockInput(bool locked)
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.enabled = !locked;
        }
        else
        {
            Debug.LogWarning("No Event System found");
        }
    }
    */

}
