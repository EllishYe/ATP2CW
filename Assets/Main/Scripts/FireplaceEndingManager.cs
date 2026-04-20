using UnityEngine;
using UnityEngine.SceneManagement;

public class FireplaceEndingManager : MonoBehaviour
{
    public static FireplaceEndingManager Instance;

    [Header("Ending Settings")]
    public SceneField sceneFrom;
    public SceneField sceneTo;
    public int requiredFullCount = 3;

    private bool backpackWasFull = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (InventoryController.Instance != null)
        {
            int count = InventoryController.Instance.GetItemCount();

            if (count >= requiredFullCount)
            {
                backpackWasFull = true;
            }
        }
    }

    public void CheckBackpackProgress()
    {

        if (InventoryController.Instance == null)
            return;

        int count = InventoryController.Instance.GetItemCount();

        if (count >= requiredFullCount)
        {
            backpackWasFull = true;
        }

        bool puzzleSolved = GameManager.Instance != null && GameManager.Instance.suitcaseUnlocked;
        bool backpackNowEmpty = InventoryController.Instance.IsEmpty();

        //Debug.Log("puzzleSolved = " + puzzleSolved);
        //Debug.Log("backpackWasFull = " + backpackWasFull);
        //Debug.Log("backpackNowEmpty = " + backpackNowEmpty);

        if (puzzleSolved && backpackWasFull && backpackNowEmpty)
        {
            SceneTransitionManager.Instance.Transition(sceneFrom, sceneTo);
        }
    }
}