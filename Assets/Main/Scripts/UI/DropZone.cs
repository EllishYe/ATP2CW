using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    //InspectableObject的物件接口，负责接收物件并且唤醒对应的显示和功能
    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag;

        if (item != null)
        {
            //唤醒对应的物件 InspectableObject对应的显示和功能：此处需要物品信息的接口
            Destroy(item);
            Debug.Log("Item Used");
        }
    }
}
