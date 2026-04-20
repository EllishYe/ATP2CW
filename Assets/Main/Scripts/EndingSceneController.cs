using UnityEngine;

public class EndingSceneController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip endingMonologue;

    void Start()
    {
        if (audioSource != null && endingMonologue != null)
        {
            audioSource.clip = endingMonologue;
            audioSource.Play();
        }
    }
}