using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        if (hits.Length == 0) return;

        // Try to find PuzzleItem first
        foreach (var hit in hits)
        {
            PuzzleItem item = hit.collider.GetComponent<PuzzleItem>();
            if (item != null)
            {
                item.OnClick();
                return;
            }
        }

        // later try to find PuzzleTrigger
        foreach (var hit in hits)
        {
            PuzzleTrigger trigger = hit.collider.GetComponent<PuzzleTrigger>();
            if (trigger != null)
            {
                trigger.OnClick();
                return;
            }
        }
    }
}