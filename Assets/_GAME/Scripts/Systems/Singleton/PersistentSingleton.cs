using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance { get => instance; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
        }
        else if (instance != GetComponent<T>())
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
