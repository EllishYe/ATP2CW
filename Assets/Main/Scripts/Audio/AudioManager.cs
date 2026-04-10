using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    
    [Header("BGM")]
    public AudioSource bgmSource;
    [Header("Past")]
    public SceneField pastScene;
    public AudioClip bgm1_Past;
    [Range(0f, 1f)]
    public float bgmPastVolume = 1f;
    [Header("Now")]
    public AudioClip bgm2_Now;
    [Range(0f, 1f)]
    public float bgmNowVolume = 1f;

    [Header("SFX")]
    public AudioSource sfxSource;

    [Header("Ambience")]
    public AudioSource ambienceSource;
    //rain
    public AudioClip amb1_Rain;
    public SceneField rainScene;
    [Range(0f, 1f)]
    public float rainVolume = 1f;
    //fire
    public AudioClip amb2_Fire;
    public SceneField fireScene;
    [Range(0f, 1f)]
    public float fireVolume = 1f;

    private AudioClip currentBGM;
    private AudioClip requestedBGM;
    private Coroutine bgmFadeCoroutine;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start()
    {
        PlayBGM(bgm2_Now);
        //PlayRain();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip targetBGM = (scene.name == pastScene.SceneName) ? bgm1_Past : bgm2_Now;

        // 不再对整个方法早退，只记录请求并按需播放 BGM
        requestedBGM = targetBGM;
        if (currentBGM != targetBGM)
        {
            PlayBGM(targetBGM);
        }

        // Ambience 处理：按场景决定目标 ambience 和音量
        AudioClip targetAmbience = null;
        float targetVolume = 1f;
        if (scene.name == rainScene.SceneName)
        {
            targetAmbience = amb1_Rain;
            targetVolume = Mathf.Clamp01(rainVolume);
        }
        else if (scene.name == fireScene.SceneName)
        {
            targetAmbience = amb2_Fire;
            targetVolume = Mathf.Clamp01(fireVolume);
        }

        if (targetAmbience != null)
        {
            // 如果 clip 不同或未在播放，确保停止并重新 Play，保证切换正确生效
            if (ambienceSource.clip != targetAmbience || !ambienceSource.isPlaying)
            {
                ambienceSource.Stop();
                ambienceSource.clip = targetAmbience;
                ambienceSource.loop = true;
                ambienceSource.volume = targetVolume;
                ambienceSource.Play();
            }
            else
            {
                // 相同 clip 且正在播放，只更新音量
                ambienceSource.volume = targetVolume;
            }
        }
        else
        {
            // 非 Rain/Fire 场景时停止 ambience
            if (ambienceSource.isPlaying)
            {
                ambienceSource.Stop();
                ambienceSource.clip = null;
            }
        }
    }
    

    public void PlayBGM(AudioClip newBGM, float fadeDuration = 1f)
    {
        if (bgmSource.clip == newBGM && bgmSource.isPlaying)return;
        //StartCoroutine(FadeBGM(newBGM, fadeDuration));

        // 如果已有未完成的淡入/淡出协程，先停止，避免并发竞态
        if (bgmFadeCoroutine != null)
        {
            StopCoroutine(bgmFadeCoroutine);
            bgmFadeCoroutine = null;
        }

        bgmFadeCoroutine = StartCoroutine(FadeBGM(newBGM, fadeDuration));
    }

    IEnumerator FadeBGM(AudioClip newBGM, float duration)
    {
        // Fade Out（从当前音量淡出）
        float previousVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= previousVolume * Time.deltaTime / duration;
            yield return null;
        }

        // 切换 clip 并播放（淡入到目标音量）
        bgmSource.clip = newBGM;
        // 根据播放的剪辑选择目标音量（使用 Inspector 中的滑块）
        float targetVolume = 1f;
        if (newBGM == bgm1_Past) targetVolume = Mathf.Clamp01(bgmPastVolume);
        else if (newBGM == bgm2_Now) targetVolume = Mathf.Clamp01(bgmNowVolume);

        bgmSource.volume = 0f;
        bgmSource.Play();

        // 只有在真正设置并开始播放时才更新 currentBGM（与 request 区分）
        currentBGM = newBGM;
        Debug.Log("Now playing BGM: " + (bgmSource.clip != null ? bgmSource.clip.name : "null"));

        // Fade In 到目标音量
        while (bgmSource.volume < targetVolume)
        {
            bgmSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }

        // 确保精确到目标音量
        bgmSource.volume = targetVolume;

        // 清理协程句柄
        bgmFadeCoroutine = null;
    }

    public void PlaySFX(AudioClip clip,float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }

    public void PlayRain()
    {
        if (ambienceSource.isPlaying) return;

        ambienceSource.clip = amb1_Rain;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }


}
