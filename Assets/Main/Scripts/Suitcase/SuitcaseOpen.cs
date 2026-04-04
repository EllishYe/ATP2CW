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
    private List<Item> childItems = new List<Item>();

    void Awake()
    {
        // 收集所有子 Item（包括未激活的）
        var items = GetComponentsInChildren<Item>(true);
        childItems.Clear();
        childItems.AddRange(items);
    }

    void Start()
    {
        // 如果某些物品已在 ObjectManager 或 GameManager 中标记为已拾取，则在场景加载时移除这些物件（确保跨场景一致）
        RemovePickedChildren();
    }

    // 打开或关闭 suitcase（控制子物件显示）
    public void SetOpen(bool open)
    {
        if (box != null)
        {
            box.SetActive(open);
        }

        foreach (var item in childItems)
        {
            if (item == null || item.gameObject == null) continue;

            // 优先使用全局的 ObjectManager（已修复为单一来源），若不存在再回退到旧的 GameManager 检查
            bool isPicked = false;
            if (ObjectManager.Instance != null)
            {
                // ObjectManager 使用 Item.itemID（枚举）为 key
                isPicked = !ObjectManager.Instance.IsItemAvailable(item.itemID);
            }
            else if (GameManager.Instance != null)
            {
                // 兼容旧逻辑：GameManager 以 string id 记录
                isPicked = GameManager.Instance.IsItemPicked(item.itemId);
            }

            if (isPicked)
            {
                Destroy(item.gameObject);
                continue;
            }

            item.gameObject.SetActive(open);
        }
    }

    public void Toggle()
    {
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
        foreach (var item in childItems.ToArray())
        {
            if (item == null) continue;

            bool isPicked = false;
            if (ObjectManager.Instance != null)
            {
                isPicked = !ObjectManager.Instance.IsItemAvailable(item.itemID);
            }
            else if (GameManager.Instance != null)
            {
                isPicked = GameManager.Instance.IsItemPicked(item.itemId);
            }

            if (isPicked)
            {
                Destroy(item.gameObject);
            }
        }

        // 重新清理列表，移除已销毁的引用
        childItems.RemoveAll(x => x == null);
    }
}
