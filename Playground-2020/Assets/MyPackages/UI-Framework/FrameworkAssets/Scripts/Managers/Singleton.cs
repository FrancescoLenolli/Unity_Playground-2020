using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    [Tooltip("Check if the object has DontDestroyOnLoad property")]
    [SerializeField] bool dontDestroyable = true;
    public static T Instance
    {
        get
        {
            //Debug.Assert(instance != null);
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (dontDestroyable) DontDestroyOnLoad(gameObject.transform.root);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
