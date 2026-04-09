using UnityEngine;

public class ClickableItem : MonoBehaviour
{
    public AudioClip clickSound;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    public void OnClick()
    {
        AudioManager.Instance.PlaySFX(clickSound, volume);
    }
}
