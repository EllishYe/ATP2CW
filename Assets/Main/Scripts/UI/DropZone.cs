using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Represents a UI drop zone that handles drag -and -drop interactions。
/// </summary>
public class DropZone : MonoBehaviour, IDropHandler
{
    //InspectableObject的物件接口，负责接收物件并且唤醒对应的显示和功能

    public InspectManager inspectManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag;

        if (item != null)
        {

            ItemDrag drag = item.GetComponent<ItemDrag>();
            if (drag != null)
            {
                drag.SetDropped();
                ItemDetails details = drag.GetItemDetails();

                if (details != null)
                {

                    inspectManager.StartInspect(details);//将物件信息传给InspectManager

                }
            }
            Debug.Log("Item Dropped");
        }
    }

}
