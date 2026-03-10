using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    public SceneField sceneFrom;
    public SceneField sceneTo;

    public void TeleportToScene()
    {
        SceneTransitionManager.Instance.Transition(sceneFrom, sceneTo);
    }

}
