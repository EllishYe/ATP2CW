using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Room1 states
    public bool GetCodeinRoom1_T1 = false;
    // 标记：Suitcase 的锁是否已被解开（解开后不再显示密码面板）
    public bool suitcaseUnlocked = false;

    // Room1 Puzzles states
    public bool[] puzzleUnlocked = new bool[4];
    public bool[] puzzleCompleted = new bool[4];
    public int?[] code = new int?[4];

    // 已被拾取并应在场景间保留的物品 ID 集合（运行时持久，GameManager 使用 DontDestroyOnLoad）
    private HashSet<string> pickedItemIds = new HashSet<string>();

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 标记物品为已拾取
    public void MarkItemPicked(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        pickedItemIds.Add(id);
    }
    // 检查物品是否已被拾取
    public bool IsItemPicked(string id)
    {
        if (string.IsNullOrEmpty(id)) return true; // 没有 id 的物品视为不可重复拾取
        return pickedItemIds.Contains(id);
    }
}