using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        yield return SceneManager.LoadSceneAsync("Persistent", LoadSceneMode.Additive);

        yield return SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Additive);

        
        SceneManager.UnloadSceneAsync("BootstrapScene");
    }
}
