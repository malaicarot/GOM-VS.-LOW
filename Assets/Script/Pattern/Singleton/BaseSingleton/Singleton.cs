using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static bool isQuit = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (isQuit) { return null; }
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    var gameObj = new GameObject(typeof(T).Name);
                    instance = gameObj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (transform.parent != null)
            {
                transform.parent = null;
            }
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isQuit = true;
    }
}
