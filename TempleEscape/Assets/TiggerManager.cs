using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerManager : MonoBehaviour
{
    public GameObject[] publicTriggerObjects;
    private static GameObject[] TriggerObjects;

    private void Start()
    {
        TriggerObjects = publicTriggerObjects; 
    }

    public static void ResetTriggers()
    {
        for (int i = 0; i < TriggerObjects.Length; i++)
        {
            TriggerObjects[i].GetComponent<ITriggerable>().Reset();
        }
    }
}
