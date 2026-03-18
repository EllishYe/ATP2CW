using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag;

        if (item != null)
        {
            Destroy(item);
            Debug.Log("Item Used");
        }
    }
}
