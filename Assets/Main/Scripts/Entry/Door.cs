using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject doorClose;
    public GameObject doorOpen;

    private bool isUnlocked = false;


    void Start()
    {
        doorClose.SetActive(true);
        doorOpen.SetActive(false);
    }

    public void OpenDoor()
    {
        doorClose.SetActive(false);
        doorOpen.SetActive(true);

        isUnlocked = true;
    }

}
