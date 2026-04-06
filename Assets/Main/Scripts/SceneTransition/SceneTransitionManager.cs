using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    //场景过渡切换管理器
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;

    // Scene from,Scene to
    public void Transition(SceneField from, SceneField to)
    {
        if(!isFade)
        StartCoroutine(TransitionToScene(from, to));
    }

    public IEnumerator TransitionToScene(SceneField from, SceneField to) {
        yield return Fade(1);//Fade out

        EventHandler.CallBeforeSceneUnloadEvent();

        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        // Set the newly loaded scene as the active scene
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();

        yield return Fade(0);//Fade in 
    }
    
    // Scene to only
    public void TransitionToScene(SceneField to)
    {
        if (!isFade)
            StartCoroutine(TransitionToScene(SceneManager.GetActiveScene().name, to));
    }
    private IEnumerator TransitionToScene(string fromSceneName, SceneField to)
    {
        yield return Fade(1);//Fade out

        EventHandler.CallBeforeSceneUnloadEvent();

        yield return SceneManager.UnloadSceneAsync(fromSceneName);
        yield return SceneManager.LoadSceneAsync(to.SceneName, LoadSceneMode.Additive);
        // Set the newly loaded scene as the active scene
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadedEvent();

        yield return Fade(0);//Fade in 
    }

    /// <summary>
    /// Fade in and Fade out
    /// </summary>
    /// <param name="targetAlpha">1 is black,0 is white</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha) {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha)) {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
