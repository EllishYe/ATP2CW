using UnityEngine;

public class SimplePuzzleProgress : MonoBehaviour
{
    [Header("Puzzle Info")]
    public int puzzleIndex;
    public int codeNumber;
    public int totalItems = 1;

    [Header("UI")]
    public GameObject hintBubble;
    public CodeUI codeUI;

    [Header("Sound")]
    public ClickableItem unlockSound;
    public ClickableItem clickSound;
    public ClickableItem solveSound;

    private int currentDone = 0;
    private bool unlocked = false;
    private bool completed = false;

    void Start()
    {
        if (hintBubble != null)
            hintBubble.SetActive(false);
    }

    public bool IsUnlocked => unlocked;
    public bool IsCompleted => completed;

    public bool TryUnlock()
    {
        if (completed) return true;

        if (!unlocked)
        {
            unlocked = true;

            if (hintBubble != null)
                hintBubble.SetActive(true);

            if (unlockSound != null)
                unlockSound.OnClick();

            Debug.Log($"Puzzle {puzzleIndex} unlocked.");
            return false; // first click only unlocks
        }

        return true; // already unlocked, allow real interaction
    }

    public void RegisterClick()
    {
        if (!unlocked || completed) return;

        currentDone++;

        if (clickSound != null)
            clickSound.OnClick();

        Debug.Log($"Puzzle {puzzleIndex}: {currentDone}/{totalItems}");

        if (currentDone >= totalItems)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        completed = true;

        if (hintBubble != null)
            hintBubble.SetActive(false);

        if (codeUI != null)
            codeUI.SetCode(puzzleIndex, codeNumber);

        if (solveSound != null)
            solveSound.OnClick();

        Debug.Log($"Puzzle {puzzleIndex} completed.");
    }
}