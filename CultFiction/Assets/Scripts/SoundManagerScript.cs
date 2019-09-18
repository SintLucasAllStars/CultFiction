using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
}
