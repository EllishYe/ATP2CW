using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance {
        get { return instance; }
    
    }

    protected virtual void Awake() {
        if (instance != null && instance != this)

            Destroy(gameObject);
        else
            instance =(T) this;
    }
    public static bool IsInitialized() {
        return instance != null;
    }
    protected virtual void onDestroy()
    {
        if (instance == this)
            instance = null;
    }

}
