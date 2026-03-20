using UnityEngine;

public class LockManager : MonoBehaviour
{
    public NumberSlot slot1;
    public NumberSlot slot2;
    public NumberSlot slot3;
    public NumberSlot slot4;

    public int[] correctPassword = new int[4] { 8, 4, 5, 3 };

    public GameObject lockContentsContainer;
    public SuitcaseOpen suitcaseOpen;

    public void CheckPassword()
    {
        int[] input = new int[4]
        {
            slot1.GetValue(),
            slot2.GetValue(),
            slot3.GetValue(),
            slot4.GetValue()
        };

        bool isCorrect = true;

        for (int i = 0; i < 4; i++)
        {
            if (input[i] != correctPassword[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("密码正确！");
            OnUnlock();
        }
        else
        {
            Debug.Log("密码错误！");
            OnFail();
        }
    }

    void OnUnlock()
    {
        // 设置全局进度：Suitcase 已解锁（之后点击不再弹出密码界面）
        if (GameManager.Instance != null)
        {
            GameManager.Instance.suitcaseUnlocked = true;
        }

        // 隐藏 LockCanvas 的子物件（但保持 LockCanvas 本身 active）
        if (lockContentsContainer != null)
        {
            lockContentsContainer.SetActive(false);
        }

        // 打开已解锁的行李箱显示（确保对象激活并打开物品显示）
        if (suitcaseOpen != null)
        {
            suitcaseOpen.gameObject.SetActive(true);
            suitcaseOpen.SetOpen(true);
        }

        Debug.Log("OnUnlock executed: suitcase unlocked and opened.");
    }

    void OnFail()
    {
        // TODO：错误反馈（抖动/红色闪烁）
    }
}