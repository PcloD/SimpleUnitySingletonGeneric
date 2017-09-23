using System;
using UnityEditor;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static readonly Type Type = typeof(T);
    private static T _instance;
    public static T Instance
    {
        get
        {
            // Try to find instance in the scene.
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                // If instance isn't found, load from resources folder.
                if (!_instance)
                {
                    // Try to find instance in resources folder
                    var tempObject = (T)Resources.Load(Type.Name, typeof(T));

                    // If found, just load from resources.
                    if (tempObject != null)
                    {
                        var tempGameObject = Instantiate(tempObject);
                        tempGameObject.name = Type.Name;
                        _instance = tempGameObject.GetComponent<T>();
                    }
                    // if not found, create a prefab and store it in resources folder.
                    else
                    {
                        var tempGameObject = new GameObject(Type.Name);
                        tempGameObject.AddComponent<T>();
                        PrefabUtility.CreatePrefab("Assets/SingletonGeneric/Resources/" + Type.Name +".prefab", tempGameObject);
                        _instance = tempGameObject.GetComponent<T>();
                        Debug.Log("Prefab created!");
                    }
                }
                // Make the object to dont destroy on load.
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this == _instance) return;

            Debug.Log("More then one instance of: " + Type.Name + " this instance will be deleted to preserve only one");
            Destroy(this.gameObject);
        }
    }
}
