using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _lock = new object();
    private static bool isAppQuitting;
    private static T instance;

    private void Start()
    {
        Application.quitting += () => isAppQuitting = true;
    }

    public static T Instance
    {
        get
        {
            if (isAppQuitting) return null;

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
