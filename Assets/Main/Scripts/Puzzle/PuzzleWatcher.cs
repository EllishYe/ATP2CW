using UnityEngine;
using System.Linq;

[DisallowMultipleComponent]
public class PuzzleWatcher : MonoBehaviour
{
    [Tooltip("当此物体下所有 PuzzleController 对应的 puzzleCompleted 全为 true 时，设置 GameManager.GetCodeinRoom1_T1 = true 并输出日志。")]
    public string debugMessage = "All puzzles in this group completed. GetCodeinRoom1_T1 set to true.";

    int[] puzzleIndices;
    bool triggered = false;

    void Start()
    {
        var pcs = GetComponentsInChildren<PuzzleController>(true);
        if (pcs == null || pcs.Length == 0)
        {
            Debug.LogWarning($"{name}: 没有在子对象中找到 PuzzleController。");
            puzzleIndices = new int[0];
            return;
        }

        puzzleIndices = pcs.Select(p => p.puzzleIndex).Distinct().ToArray();
    }

    void Update()
    {
        if (triggered) return;
        if (puzzleIndices == null || puzzleIndices.Length == 0) return;
        if (GameManager.Instance == null) return;

        // 检查所有索引对应的完成状态
        bool allDone = true;
        foreach (int idx in puzzleIndices)
        {
            // 防御性检查索引范围
            if (idx < 0 || idx >= GameManager.Instance.puzzleCompleted.Length)
            {
                allDone = false;
                break;
            }

            if (!GameManager.Instance.puzzleCompleted[idx])
            {
                allDone = false;
                break;
            }
        }

        if (allDone)
        {
            GameManager.Instance.GetCodeinRoom1_T1 = true;
            Debug.Log(debugMessage);
            triggered = true;
        }
    }
}
