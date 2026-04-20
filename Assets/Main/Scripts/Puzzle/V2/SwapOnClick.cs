using UnityEngine;

public class SwapOnClick : MonoBehaviour
{
    public GameObject replacementObject;
    public PuzzleController controller;
    public bool countAsCollected = true;

    private bool clicked = false;

    public void OnClick()
    {
        if (clicked) return;
        if (controller == null || GameManager.Instance == null) return;
        if (!controller.IsUnlocked) return;
        if (GameManager.Instance.puzzleCompleted[controller.puzzleIndex]) return;

        clicked = true;

        if (replacementObject != null)
            replacementObject.SetActive(true);

        gameObject.SetActive(false);

        if (countAsCollected)
            controller.OnItemCollected();
    }
}