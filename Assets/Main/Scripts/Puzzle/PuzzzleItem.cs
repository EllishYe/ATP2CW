using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public PuzzleController controller;

    [Header("Persistent ID")]
    public string persistentId;
    [Tooltip("Index in the Puzzle")]
    public int pieceIndex = 0;

    void Awake()
    {
        // 如果没有手动设置，则根据 puzzleIndex + pieceIndex 自动生成稳定 ID
        if (string.IsNullOrEmpty(persistentId))
        {
            var pIndex = controller != null ? controller.puzzleIndex : -1;
            persistentId = $"puzzle_{pIndex}_{pieceIndex}";
        }
    }

    void Start()
    {
        // 启动时询问全局管理器是否已被收集，若已收集则隐藏（避免再次出现）
        if (ObjectManager.Instance != null && ObjectManager.Instance.IsPuzzleItemCollected(persistentId))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {

        if (controller == null || GameManager.Instance == null)
            return;

        if (!controller.IsUnlocked)
        {
            controller.UnlockPuzzle();
            return;
        }

        if (!GameManager.Instance.puzzleUnlocked[controller.puzzleIndex])
            return;

        controller.OnItemCollected();
        gameObject.SetActive(false);

        // 将收集状态记录到全局 ObjectManager（持久跨场景）
        if (ObjectManager.Instance != null)
            ObjectManager.Instance.MarkPuzzleItemCollected(persistentId);
    }
}
