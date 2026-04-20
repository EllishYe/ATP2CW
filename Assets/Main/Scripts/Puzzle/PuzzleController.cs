using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public int puzzleIndex;        // 0~3
    public int codeNumber;        // 对应密码
    public int manualTotalItems = 0;

    public GameObject hintBubble;
    public CodeUI codeUI;

    int totalItems;
    int currentDone;

    public ClickableItem unlockSound;
    public ClickableItem clickSound;
    public ClickableItem solveSound;


    void Start()
    {
        if (manualTotalItems > 0)
            totalItems = manualTotalItems;
        else
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

        bool wasUnlocked = GameManager.Instance.puzzleUnlocked[puzzleIndex];

        GameManager.Instance.puzzleUnlocked[puzzleIndex] = true;
        hintBubble.SetActive(true);
        if (!wasUnlocked)
        {
            unlockSound.OnClick();
        }
    }

    public void OnItemCollected()
    {
        currentDone++;
        clickSound.OnClick();

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
        
        solveSound.OnClick();
    }
}