using UnityEngine;

public class StarPairClick : MonoBehaviour
{
    public GameObject linkedImage;
    public PuzzleController controller;

    private bool clicked = false;

    public void OnClick()
    {
        if (clicked) return;
        if (controller == null || GameManager.Instance == null) return;
        if (!controller.IsUnlocked) return;
        if (GameManager.Instance.puzzleCompleted[controller.puzzleIndex]) return;

        clicked = true;

        if (linkedImage != null)
            linkedImage.SetActive(false);

        gameObject.SetActive(false);

        controller.OnItemCollected();
    }
}