using UnityEngine;

public class HoldButtonController : MonoBehaviour
{
    public HoldButton holdbutton;
    public GameObject buttonRoot;

    public void ShowHoldButton()
    {
        buttonRoot.SetActive(true);
        holdbutton.ResetButton();
    }
}
