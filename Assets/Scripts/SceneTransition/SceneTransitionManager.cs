using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    public void Transition(SceneField from,SceneField to)
    {
        StartCoroutine(TransitionToScene(from, to));
    }

    public IEnumerator TransitionToScene(SceneField from, SceneField to) {
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        // Set the newly loaded scene as the active scene
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}
