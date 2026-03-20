using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public PuzzleController controller;

    
    public void OnClick()
    {
        if (!GameManager.Instance.puzzleUnlocked[controller.puzzleIndex])
            return;

        if (GameManager.Instance.puzzleCompleted[controller.puzzleIndex])
            return;

        controller.OnItemCollected();
        gameObject.SetActive(false);
    }
}
