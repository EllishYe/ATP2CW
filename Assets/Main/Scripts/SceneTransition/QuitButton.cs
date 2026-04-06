using UnityEngine;

public class QuitButton : MonoBehaviour
{
    
    public SceneField sceneTo; // Set to StartScene

    public void QuitToStartScene()
    {
        if (sceneTo == null || string.IsNullOrEmpty(sceneTo.SceneName))
        {
            Debug.LogWarning("QuitButton: sceneTo Œ¥…Ë÷√ªÚ SceneName Œ™ø’°£");
            return;
        }

        SceneTransitionManager.Instance.TransitionToScene(sceneTo);
    }
}
