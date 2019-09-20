using UnityEngine;
using System;
using System.Collections;

public class extendedFunctions : MonoBehaviour
{
    /// <summary>
    /// waits for the given time
    /// </summary>
    /// <param name="wait">amount of time to wait</param>
    /// <param name="action">callback</param>
    public void Wait(float wait, Action action)
    {
        StartCoroutine(_wait(wait, action));
    }

    IEnumerator _wait(float wait, Action callback)
    {
        yield return new WaitForSecondsRealtime(wait);
        callback();
    }

    public static float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}