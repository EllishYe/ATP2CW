using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public GameObject hintBubble;
    public ClickableItem clickSound;

    void OnMouseDown()
    {
        hintBubble.SetActive(!hintBubble.activeSelf);
        clickSound.OnClick();
    }
}
