using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingletonScriptableObject <T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T Instance()
    {
        if (_instance == null)
        {
            _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();

            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<T>();
            }
        }
        return _instance;
    }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
    }
}
