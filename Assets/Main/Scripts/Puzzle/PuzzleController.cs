using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public int puzzleIndex;        // 0~3
    public int codeNumber;        // ∂‘”¶√‹¬Î

    public GameObject hintBubble;
    public CodeUI codeUI;

    int totalItems;
    int currentDone;

    void Start()
    {
        totalItems = GetComponentsInChildren<PuzzleItem>().Length;

        // ∂¡»°¥Êµµ◊¥Ã¨
        if (GameManager.Instance.puzzleCompleted[puzzleIndex])
        {
            CompletePuzzle();
        }
        else if (GameManager.Instance.puzzleUnlocked[puzzleIndex])
        {
            hintBubble.SetActive(true);
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