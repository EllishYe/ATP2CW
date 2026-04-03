using UnityEngine;

public class FireInputTest : MonoBehaviour
{
    [Header("Animator Reference")]
    public Animator animator;

    [Header("Trigger Name")]
    public string burnTriggerName = "BurnTrigger";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TriggerBurn();
        }
    }

    public void TriggerBurn()
    {
        if (animator != null)
        {
            animator.SetTrigger(burnTriggerName);
        }
        else
        {
            Debug.LogWarning("Animator not assigned!");
        }
    }
}
