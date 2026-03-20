using UnityEngine;
using TMPro;

public class NumberSlot : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    private int currentValue = 0;

    void Start()
    {
        UpdateText();
    }

    public void OnClick()
    {
        currentValue++;

        if (currentValue > 9)
            currentValue = 0;

        UpdateText();
    }

    void UpdateText()
    {
        numberText.text = currentValue.ToString();
    }

    public int GetValue()
    {
        return currentValue;
    }
}