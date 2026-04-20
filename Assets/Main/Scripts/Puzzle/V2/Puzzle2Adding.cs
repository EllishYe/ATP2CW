using UnityEngine;

public class Puzzle2Adding : MonoBehaviour
{
    public PuzzleController controller;
    public GameObject[] itemsToShow;

    private bool shown = false;

    public void OnClick()
    {
        if (shown) return;
        if (controller == null || GameManager.Instance == null) return;
        if (!GameManager.Instance.puzzleUnlocked[controller.puzzleIndex]) return;

        shown = true;

        foreach (GameObject obj in itemsToShow)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        Debug.Log("Puzzle2Adding shown once.");
    }
}