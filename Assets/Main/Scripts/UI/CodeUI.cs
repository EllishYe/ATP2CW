using UnityEngine;
using TMPro;

public class CodeUI : MonoBehaviour
{
    public TextMeshProUGUI[] slots; // 4

    int?[] code = new int?[4];

    void Start()
    {
        code = GameManager.Instance.code;
        UpdateUI();
    }

    public void SetCode(int index, int number)
    {
        GameManager.Instance.code[index] = number;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (code[i].HasValue)
                slots[i].text = code[i].Value.ToString();
            else
                slots[i].text = "_";
        }
    }
}