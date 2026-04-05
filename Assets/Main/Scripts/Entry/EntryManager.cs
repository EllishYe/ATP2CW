using UnityEngine;

public class EntryManager : MonoBehaviour
{
    [Header("Refs")]
    public Animator cameraAnimator;
    public GameObject startUI;     // 整个UI（按钮+标题）
    public Door door;

    [Header("State")]
    private bool isTransitioning = false;


    public void OnClickStart()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        cameraAnimator.SetTrigger("ZoomTrigger");
    }
    public void OnZoomFinished()
    {
        door.OpenDoor();
    }
}
