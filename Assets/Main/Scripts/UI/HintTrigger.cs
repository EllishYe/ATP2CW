using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public GameObject hintBubble;

    void OnMouseDown()
    {
        hintBubble.SetActive(!hintBubble.activeSelf);
    }
}
