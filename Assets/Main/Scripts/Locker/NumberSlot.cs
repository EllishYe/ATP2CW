using UnityEngine;
using TMPro;

/// <summary>
/// display a number(0~9) and allow player to click to change it.Used in LockCanvas for password input.
/// </summary>
public class NumberSlot : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    private int currentValue = 0;

    public LockManager lockManager;// notify lock manager to check password when value changes

    void Awake()
    {
        if (lockManager == null)
        {
            lockManager = GetComponentInParent<LockManager>();
            if (lockManager == null)
            {
                Debug.LogWarning($"{name}: Can't find LockManager in Parent GameObject, plz assign it manually");
            }
        }
    }
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
        
        // check password whenever the value changes
        if (lockManager != null)
        {
            lockManager.CheckPassword();
        }
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