using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                { 
                    GameObject game = new GameObject(typeof(T).Name);
                    game.name = typeof(T).Name;
                    instance = game.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

}