using UnityEngine;

public class InspectManager : MonoBehaviour
{
    //public GameObject scratchCanvas;
    //public GameObject confirmCanvas;
    //public GameObject dissolveController;
    //public GameObject fireController;

    private ItemDetails currentItem;

    public DropZone dropzone;// Use Canvas
    public HoldButtonController confirmCanvas;// Confirm Canvas
    public ScratchCardCanvasController scratchCanvas;// Scratch Canvas
    public DissolveController dissolveController;// Dissolve Controller



    public void StartInspect(ItemDetails item)
    {
        currentItem = item;

        scratchCanvas.ReceiveItem(item);
        scratchCanvas.Show(scratchCanvas.frontImage);
        scratchCanvas.Show(scratchCanvas.backImage);
    }

    public void OnScratchFinished()
    {
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
        //fireController.TriggerBurn();

        //LockInput(false);

        //Debug.Log("Inspect Finished");
    }
}
