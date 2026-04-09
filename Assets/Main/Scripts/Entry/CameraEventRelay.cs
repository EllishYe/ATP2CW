using UnityEngine;

public class CameraEventRelay : MonoBehaviour
{
    public EntryManager entryManager;

    public void OnZoomFinished()
    {
        entryManager.OnZoomFinished();
    }

    //Play the sound effect when the door opens
    public AudioClip openSound;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    public void PlayOpenSound()
    {
        AudioManager.Instance.PlaySFX(openSound, volume);
    }
}
