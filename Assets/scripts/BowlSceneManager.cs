using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlSceneManager : MonoBehaviour
{

    private WaveManager waveManager;

    private void Start()
    {
        waveManager = GetComponent<WaveManager>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            waveManager.StartWave();
        }
    }
}
