using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance = null;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = gameObject.GetComponent<T>();
        else if (Instance.GetInstanceID() != GetInstanceID())
            Destroy(gameObject);
    }
}