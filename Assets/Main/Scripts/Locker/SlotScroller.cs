using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScroller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform content;

    public float itemHeight = 100f; // 每个数字高度
    public float snapSpeed = 10f;
    public int digits = 10; // 数字个数，默认 0..9

    private Vector2 startDragPos;
    private Vector2 contentStartPos;

    private bool isDragging = false;
    private bool isSnapping = false;

    private float targetY;

    void Start()
    {
        // 确保初始位置在合理范围内（避免位置跑到很大值）
        if (content != null)
        {
            LoopCheck();
        }
    }

    void Update()
    {
        if (isSnapping)
        {
            Vector3 pos = content.anchoredPosition;
            pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * snapSpeed);

            content.anchoredPosition = pos;

            if (Mathf.Abs(pos.y - targetY) < 0.1f)
            {
                pos.y = targetY;
                content.anchoredPosition = pos;
                isSnapping = false;
            }
        }

        // 非拖拽、非吸附时执行环绕检查以维持循环效果
        if (!isSnapping && !isDragging)
        {
            LoopCheck();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        isSnapping = false;

        startDragPos = eventData.position;
        contentStartPos = content.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - startDragPos.y;

        Vector2 newPos = contentStartPos + new Vector2(0, deltaY);

        // 允许无限拖拽（靠 LoopCheck 做环绕），取消原有的硬性 Clamp
        content.anchoredPosition = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        SnapToClosest();
    }

    void SnapToClosest()
    {
        float currentY = content.anchoredPosition.y;

        int index = Mathf.RoundToInt(currentY / itemHeight);

        // 计算目标位置（不在这里做模，需要保留可环绕的大索引）
        targetY = index * itemHeight;
        isSnapping = true;
    }

    // 获取当前选中的数字
    public int GetValue()
    {
        int index = Mathf.RoundToInt(content.anchoredPosition.y / itemHeight);
        return Mod(index, digits);
    }

    // 强制设置值并立即吸附到该值（用于初始化）
    public void SetValue(int value, bool notify = true)
    {
        int v = Mathf.Clamp(value, 0, digits - 1);
        // 把位置设置到中间那一组，减少立刻触发环绕的概率
        float oneSet = digits * itemHeight;
        float middleBase = oneSet; // 中间组的偏移
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, middleBase + v * itemHeight);
        targetY = Mathf.Round(content.anchoredPosition.y / itemHeight) * itemHeight;
        isSnapping = false;
    }

    // 保持 content 在合理区间并做循环（基于一组高度）
    void LoopCheck()
    {
        if (content == null || digits <= 0) return;

        float oneSetHeight = digits * itemHeight;
        float y = content.anchoredPosition.y;

        // 把 y 保持在 [oneSetHeight, 2 * oneSetHeight) 区间（中间那组）
        // 如果超出则减/加一组高度以环绕
        if (y >= oneSetHeight * 2f)
        {
            y -= oneSetHeight;
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, y);
        }
        else if (y < oneSetHeight)
        {
            y += oneSetHeight;
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, y);
        }
    }

    int Mod(int x, int m)
    {
        int r = x % m;
        if (r < 0) r += m;
        return r;
    }

    // 原来的调试/备份代码保留（注释）
    //void Start()
    //{
    //    content.anchoredPosition = new Vector2(0, 10 * itemHeight);
    //}
    //
    //void LoopCheck()
    //{
    //    float y = content.anchoredPosition.y;
    //
    //    float oneSetHeight = 10 * itemHeight;
    //
    //    // ⭐ 如果跑到“上面那组”
    //    if (y > oneSetHeight * 2)
    //    {
    //        y -= oneSetHeight;
    //    }
    //    // ⭐ 如果跑到“下面那组”
    //    else if (y < oneSetHeight)
    //    {
    //        y += oneSetHeight;
    //    }
    //
    //    content.anchoredPosition = new Vector2(0, y);
    //}
}