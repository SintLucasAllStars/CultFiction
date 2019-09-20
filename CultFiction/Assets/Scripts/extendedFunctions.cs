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
}