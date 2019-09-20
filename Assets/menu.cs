using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public static menu instance;
    public playModes playmode;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
}
