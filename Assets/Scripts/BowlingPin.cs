using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    private PinSpawner _pinSpawner;

    private void Start()
    {
        _pinSpawner = GameObject.FindObjectOfType<PinSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BowlingBall>())
        {
            _pinSpawner.SendPinSpawnSignal();
        }
    }
}
