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

    void Start()
    {
        ApplyState(SceneManager.GetActiveScene());
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
        ApplyState(scene);
    }

    void ApplyState(Scene scene)
    {
        if (entries == null || entries.Count == 0) return;

        string currentSceneName = scene.name;

        foreach (var entry in entries)
        {
            if (entry == null) continue;

            var go = entry.target;
            if (go == null)
            {
                Debug.LogWarning($"CanvasSceneController: 条目 target 未绑定，跳过。");
                continue;
            }

            bool shouldHide = false;
            if (entry.hideInScenes != null)
            {
                foreach (var sf in entry.hideInScenes)
                {
                    if (sf == null) continue;
                    // SceneField 支持隐式转换为 string，也可以使用 sf.SceneName
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
