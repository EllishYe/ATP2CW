using UnityEngine;

public class ScratchAutoClear : MonoBehaviour
{
    [Tooltip("指向场景中负责计算进度的 EraseProgress（可为空，会自动查找同物体上的组件）")]
    public ScratchCardAsset.EraseProgress eraseProgress;

    [Tooltip("要在进度达到阈值时隐藏的目标（通常是 ImageCard 的根 GameObject）")]
    public GameObject targetToHide;

    [Range(0f, 1f)]
    public float threshold = 0.85f;

    [Tooltip("只触发一次（隐藏后不再响应）")]
    public bool onlyOnce = true;

    bool triggered = false;

    void Reset()
    {
        if (eraseProgress == null)
            eraseProgress = GetComponent<ScratchCardAsset.EraseProgress>();
    }

    void OnEnable()
    {
        // 每次启用时重置触发器并确保目标可见（保证下一次可再次触发）
        triggered = false;

        if (eraseProgress != null)
        {
            eraseProgress.OnProgress += OnProgress;
            eraseProgress.OnCompleted += OnCompleted;
        }

        // 确保前端目标在启用时可见（上一次可能被隐藏）
        GameObject go = targetToHide;
        if (go == null && eraseProgress != null && eraseProgress.Card != null && eraseProgress.Card.SurfaceTransform != null)
            go = eraseProgress.Card.SurfaceTransform.gameObject;

        if (go != null)
            go.SetActive(true);
    }

    void OnDisable()
    {
        if (eraseProgress != null)
        {
            eraseProgress.OnProgress -= OnProgress;
            eraseProgress.OnCompleted -= OnCompleted;
        }
    }

    void OnProgress(float progress)
    {
        if (triggered && onlyOnce) return;

        // 当 progress 到达或超过阈值时隐藏目标
        if (progress >= threshold)
        {
            DoHide();
        }
    }

    void OnCompleted(float progress)
    {
        if (triggered && onlyOnce) return;
        // 额外保险：当组件报告完成时也隐藏
        DoHide();
    }

    void DoHide()
    {
        GameObject go = targetToHide;
        if (go == null && eraseProgress != null && eraseProgress.Card != null && eraseProgress.Card.SurfaceTransform != null)
            go = eraseProgress.Card.SurfaceTransform.gameObject;

        if (go != null)
            go.SetActive(false);

        triggered = true;
        
        //触发事件通知外部（InspectectManager）擦除已完成，可以进行下一步
        EventHandler.CallScratchClearedEvent();
    }
}
