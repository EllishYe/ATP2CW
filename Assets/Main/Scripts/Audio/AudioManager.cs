using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;

    public AudioSource sfxSource;

    protected override void Awake()
    {
        base.Awake();
        // 这里放其他初始化代码（如果有），例如确保 bgmSource 不为 null 等

        if (sfxSource == null)
        {
            GameObject sfxGO = new GameObject("SFX_Source");
            sfxGO.transform.SetParent(transform);
            sfxSource = sfxGO.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }
    }

    /*
    void Awake()
    {
        //singleton pattern to ensure only one instance od audiomanager exits
        if (instance == null)
        {
            instance = this;
            dontdestroyonload(gameobject);
        }
        else
        {
            destroy(gameobject);
        }
    }
    */

    public void PlayBGM(AudioClip newBGM, float fadeDuration = 1f)
    {
        StartCoroutine(FadeBGM(newBGM, fadeDuration));
    }

    IEnumerator FadeBGM(AudioClip newBGM, float duration)
    {
        // Fade Out
        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        bgmSource.clip = newBGM;
        bgmSource.Play();

        // Fade In
        while (bgmSource.volume < startVolume)
        {
            bgmSource.volume += startVolume * Time.deltaTime / duration;
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip,float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }


}
