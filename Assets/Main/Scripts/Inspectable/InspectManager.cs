using UnityEngine;
using System.Collections;
using ScratchCardAsset;

public class InspectManager : MonoBehaviour
{

    private ItemDetails currentItem;

    public DropZone dropzone;// Use Canvas
    public HoldButtonController confirmCanvas;// Confirm Canvas
    public ScratchCardCanvasController scratchCanvas;// Scratch Canvas
    public ScratchCardManager scratchManager; // Scratch Manager
    public DissolveController dissolveController;// Dissolve Controller
    public FireInputTest fireController;// Fire Contoller



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
    }

    public void OnConfirmFinished()
    {
        //LockInput(true); 开始禁用玩家输入

        //InventorySlotManager.Instance.RemoveCurrentItem();在InventoryManager和InventorySlotManager中实现Data和UI的同步，移除当前物品

        dissolveController.PlayDissolve(); //将BackImage的Dissolve效果播放出来

    }

    public void OnDissolveFinished()
    {
        fireController.TriggerBurn();

        //LockInput(false);

        //Debug.Log("Inspect Finished");
    }
}
