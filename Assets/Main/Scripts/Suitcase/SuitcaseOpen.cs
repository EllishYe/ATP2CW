using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SuitcaseOpen : MonoBehaviour
{
    [Header("Item Holder Box")]
    public GameObject box;

    [Header("Awake State")]
    public bool startOpen = false;

    // 查找的子物品（带 ItemController 组件）
    private List<ItemController> childItems = new List<ItemController>();

    void Awake()
    {
        // 收集所有子 ItemController（包括未激活的）
        var items = GetComponentsInChildren<ItemController>(true);
        childItems.Clear();
        childItems.AddRange(items);
    }

    void Start()
    {
        // 如果某些物品已在 GameManager 中标记为已拾取，则在场景加载时移除这些物件（确保跨场景一致）
        RemovePickedChildren();

        // 根据 startOpen 设置初始显示
        //SetOpen(startOpen);
    }

    // 打开或关闭 suitcase（控制子物件显示）
    public void SetOpen(bool open)
    {
        //Debug.Log("1");
        // 对 box：如果绑定则设置为 open（box 可随开关隐藏/显示）
        if (box != null)
        {
            box.SetActive(open);
        }

        // 对其他子物品：若尚未被拾取，则根据 open 设置显示；若已被拾取则确保不存在
        foreach (var item in childItems)
        {
            //Debug.Log("2");
            if (item == null || item.gameObject == null) continue;

            // 若物品已被 GameManager 标记为拾取，则销毁该对象（可能在切回场景时重新实例化）
            if (GameManager.Instance != null && GameManager.Instance.IsItemPicked(item.itemId))
            {
                Destroy(item.gameObject);
                continue;
            }

            item.gameObject.SetActive(open);
        }
    }

    public void Toggle()
    {
        // 判断当前 box / 第一个子物的状态以决定切换方向
        bool currentlyOpen = true;

        if (box != null)
            currentlyOpen = box.activeSelf;
        else if (childItems.Count > 0 && childItems[0] != null)
            currentlyOpen = childItems[0].gameObject.activeSelf;

        SetOpen(!currentlyOpen);
    }

    // 在场景开始时移除已被拾取的子物件（避免在切换场景后再次出现）
    void RemovePickedChildren()
    {
        if (GameManager.Instance == null) return;

        foreach (var item in childItems.ToArray())
        {
            if (item == null) continue;
            if (GameManager.Instance.IsItemPicked(item.itemId))
            {
                Destroy(item.gameObject);
            }
        }

        // 重新清理列表，移除已销毁的引用
        childItems.RemoveAll(x => x == null);
    }
}
