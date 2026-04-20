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
        
        // If clicking anything inside a locked puzzle, unlock it first.
        foreach (var hit in hits)
        {
            PuzzleTrigger trigger = hit.collider.GetComponentInParent<PuzzleTrigger>();
            if (trigger != null && trigger.controller != null)
            {
                if (!GameManager.Instance.puzzleCompleted[trigger.controller.puzzleIndex] &&
                    !GameManager.Instance.puzzleUnlocked[trigger.controller.puzzleIndex])
                {
                    trigger.OnClick();

                    // If this click is inside Puzzle2, also run Puzzle2Adding immediately
                    Puzzle2Adding area = hit.collider.GetComponentInParent<Puzzle2Adding>();
                    if (area != null)
                    {
                        area.OnClick();
                    }

                    return;
                }
            }

            PuzzleItem item = hit.collider.GetComponentInParent<PuzzleItem>();
            if (item != null && item.controller != null)
            {
                if (!GameManager.Instance.puzzleCompleted[item.controller.puzzleIndex] &&
                    !GameManager.Instance.puzzleUnlocked[item.controller.puzzleIndex])
                {
                    item.controller.UnlockPuzzle();

                    Puzzle2Adding area = hit.collider.GetComponentInParent<Puzzle2Adding>();
                    if (area != null)
                    {
                        area.OnClick();
                    }

                    return;
                }
            }

            StarPairClick star = hit.collider.GetComponentInParent<StarPairClick>();
            if (star != null && star.controller != null)
            {
                if (!GameManager.Instance.puzzleCompleted[star.controller.puzzleIndex] &&
                    !GameManager.Instance.puzzleUnlocked[star.controller.puzzleIndex])
                {
                    star.controller.UnlockPuzzle();

                    Puzzle2Adding area = hit.collider.GetComponentInParent<Puzzle2Adding>();
                    if (area != null)
                    {
                        area.OnClick();
                    }

                    return;
                }
            }

            SwapOnClick swap = hit.collider.GetComponentInParent<SwapOnClick>();
            if (swap != null && swap.controller != null)
            {
                if (!GameManager.Instance.puzzleCompleted[swap.controller.puzzleIndex] &&
                    !GameManager.Instance.puzzleUnlocked[swap.controller.puzzleIndex])
                {
                    swap.controller.UnlockPuzzle();

                    Puzzle2Adding area = hit.collider.GetComponentInParent<Puzzle2Adding>();
                    if (area != null)
                    {
                        area.OnClick();
                    }

                    return;
                }
            }
        }



        // 1. Swap first
        foreach (var hit in hits)
        {
            SwapOnClick swap = hit.collider.GetComponent<SwapOnClick>();
            if (swap != null)
            {
                //Debug.Log("Running SwapOnClick on " + hit.collider.gameObject.name);
                swap.OnClick();
                return;
            }
        }

        // 2. Star pair second
        foreach (var hit in hits)
        {
            StarPairClick star = hit.collider.GetComponent<StarPairClick>();
            if (star != null)
            {
                //Debug.Log("Running StarPairClick on " + hit.collider.gameObject.name);
                star.OnClick();
                return;
            }
        }

        // 3. Puzzle item
        foreach (var hit in hits)
        {
            PuzzleItem item = hit.collider.GetComponent<PuzzleItem>();
            if (item != null)
            {
                //Debug.Log("Running PuzzleItem on " + hit.collider.gameObject.name);
                item.OnClick();
                return;
            }
        }

        // 4. Area / trigger logic last
        foreach (var hit in hits)
        {
            PuzzleTrigger trigger = hit.collider.GetComponent<PuzzleTrigger>();
            if (trigger != null)
            {
                //Debug.Log("Running PuzzleTrigger on " + hit.collider.gameObject.name);
                trigger.OnClick();
            }

            Puzzle2Adding area = hit.collider.GetComponent<Puzzle2Adding>();
            if (area != null)
            {
                //Debug.Log("Running Puzzle2Adding on " + hit.collider.gameObject.name);
                area.OnClick();
            }
        }
    }
}