using UnityEngine;

public class CameraEventRelay : MonoBehaviour
{
    public EntryManager entryManager;

    public void OnZoomFinished()
    {
        entryManager.OnZoomFinished();
    }
}
