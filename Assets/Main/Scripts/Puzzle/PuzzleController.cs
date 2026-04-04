using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public int puzzleIndex;        // 0~3
    public int codeNumber;        // 对应密码

    public GameObject hintBubble;
    public CodeUI codeUI;

    int totalItems;
    int currentDone;

    void Start()
    {
        totalItems = GetComponentsInChildren<PuzzleItem>().Length;

        // 读取存档状态
        if (GameManager.Instance.puzzleCompleted[puzzleIndex])
        {
            CompletePuzzle();
        }
        else if (GameManager.Instance.puzzleUnlocked[puzzleIndex])
        {
            hintBubble.SetActive(true);
        }
    }

    // 只读属性，便于外部（例如 PuzzleItem）查询当前是否已解锁
    public bool IsUnlocked
    {
        get
        {
            return GameManager.Instance != null && GameManager.Instance.puzzleUnlocked[puzzleIndex];
        }
    }

    public void UnlockPuzzle()
    {
        if (GameManager.Instance.puzzleCompleted[puzzleIndex])
            return;

        GameManager.Instance.puzzleUnlocked[puzzleIndex] = true;
        hintBubble.SetActive(true);
    }

    public void OnItemCollected()
    {
        currentDone++;

        if (currentDone >= totalItems)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        GameManager.Instance.puzzleCompleted[puzzleIndex] = true;

        hintBubble.SetActive(false);

        codeUI.SetCode(puzzleIndex, codeNumber);

        Debug.Log("Puzzle " + puzzleIndex + " Completed!");
    }
}