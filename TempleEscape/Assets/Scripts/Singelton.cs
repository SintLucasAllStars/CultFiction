using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance = null;

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this.gameObject.GetComponent<T>();
        else if (Instance.GetInstanceID() != GetInstanceID())
            Destroy(this.gameObject);
    }
}
