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
    }
    void OnDisable()
    {
        EventHandler.ScratchClearedEvent -= OnScratchCleared_Event;
        EventHandler.ConfirmFinishedEvent -= OnConfirmFinished_Event;
    }
    void OnScratchCleared_Event()
    {
        OnScratchFinished();
    }
    void OnConfirmFinished_Event()
    {
        OnConfirmFinished();
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
        //reset scratch manager中关于front image的Erase Progress的数据方便下次使用
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
        //先将dissolveController控制的BackImage视觉隐藏掉，再重置里面的DissolveAmount参数以便下次使用
        fireController.TriggerBurn();

        //LockInput(false);

        //Debug.Log("Inspect Finished");
    }
}
