using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleController controller;
    public void OnClick()
    {
        if (GameManager.Instance.puzzleCompleted[controller.puzzleIndex])
            return;

        controller.UnlockPuzzle();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}