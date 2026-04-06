using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSceneController : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        [Tooltip("要控制的对象（可以是 Canvas 或任意 GameObject）")]
        public GameObject target;

        [Tooltip("当活动场景位于此列表任一场景时，将隐藏 target。留空表示永远不隐藏。")]
        public List<SceneField> hideInScenes = new List<SceneField>();
    }

    [Header("Custom target -> multiple hide scenes mapping")]
    [Tooltip("为每个要受场景隐藏/显示控制的对象添加一个条目。")]
    public List<Entry> entries = new List<Entry>();

    // 标志：是否已经通过 OnSceneLoaded 收到场景加载回调（用于防止 Start 覆盖 OnSceneLoaded 设置）
    private bool _hasReceivedSceneLoaded = false;

    void Start()
    {
        // 如果尚未收到任何 sceneLoaded 回调，延迟一帧再应用状态以避免与同帧的 OnSceneLoaded/其它初始化冲突
        if (!_hasReceivedSceneLoaded)
        {
            StartCoroutine(DelayedApplyOneFrame());
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _hasReceivedSceneLoaded = true;
        ApplyState(scene);
    }

    IEnumerator DelayedApplyOneFrame()
    {
        yield return null; // 等一帧，等待所有 Awake/Start/sceneLoaded（若发生）先执行
        if (_hasReceivedSceneLoaded) yield break;

        var scene = SceneManager.GetActiveScene();
        ApplyState(scene);
    }

    void ApplyState(Scene scene)
    {
        if (entries == null || entries.Count == 0) return;

        string currentSceneName = scene.name;

        for (int i = 0; i < entries.Count; i++)
        {
            var entry = entries[i];
            if (entry == null) continue;

            var go = entry.target;
            if (go == null) continue;

            bool shouldHide = false;
            if (entry.hideInScenes != null)
            {
                foreach (var sf in entry.hideInScenes)
                {
                    if (sf == null) continue;
                    if (currentSceneName == sf.SceneName)
                    {
                        shouldHide = true;
                        break;
                    }
                }
            }

            go.SetActive(!shouldHide);
        }
    }
}