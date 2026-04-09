using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class SuitcaseInteraction : MonoBehaviour, IPointerClickHandler
{
    public GameObject lockContentsContainer;
    public SuitcaseOpen suitcaseOpen;
    public GameObject hintbubble;
    public ClickableItem clickableItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 保持对 UI 事件的兼容，复用同一处理函数
        OnSuitcaseClicked();
    }

    // 子对象的转发器或外部代码也能调用（不依赖 EventData）
    public void OnSuitcaseClicked()
    {
        clickableItem.OnClick();
        if (GameManager.Instance == null) return;

        // 如果还未触发房间密码（无法交互）
        if (!GameManager.Instance.GetCodeinRoom1_T1)
        {
            Debug.Log("Suitcase: 还未获得房间密码，无法打开。");
            //改成显示LockCanvas
            if (lockContentsContainer != null)
            {
                lockContentsContainer.SetActive(true);
            }
            //并且显示提示的Bubble
            hintbubble.SetActive(true);
            return;
        }

        // 如果尚未解锁（第一次允许显示密码界面）
        if (!GameManager.Instance.suitcaseUnlocked)
        {
            // 显示 LockCanvas 的子项（密码界面）
            if (lockContentsContainer != null)
            {
                lockContentsContainer.SetActive(true);
            }
            // LockManager 负责在解锁时将 lockContentsContainer 关闭并激活 suitcaseOpen
            return;
        }

        // 如果已解锁，则切换已打开行李箱的显示（打开/关闭）
        if (suitcaseOpen != null)
        {
            if (!suitcaseOpen.gameObject.activeSelf)
            {
                suitcaseOpen.gameObject.SetActive(true);
                suitcaseOpen.SetOpen(true);
            }
            else
            {
                suitcaseOpen.SetOpen(false);
            }
        }
    }
}
