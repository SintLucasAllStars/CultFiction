using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _pins;

    [SerializeField]
    private GameObject _currentPins;

    [SerializeField]
    private float _fallDelay = 10.0f;

    [SerializeField]
    private float _replaceDelay = 2.0f;

    private bool _signalReceived = false;

    public void SendPinSpawnSignal()
    {
        if (!_signalReceived)
        {
            _signalReceived = true;
            Invoke("RemovePinCollisions", _fallDelay);
            Invoke("SwapOutNewPins", _fallDelay + _replaceDelay);
        }
    }

    private void RemovePinCollisions()
    {
        foreach (BoxCollider collider in _currentPins.GetComponentsInChildren<BoxCollider>())
        {
            Destroy(collider);
        }

        foreach (CapsuleCollider collider in _currentPins.GetComponentsInChildren<CapsuleCollider>())
        {
            Destroy(collider);
        }
    }

    private void SwapOutNewPins()
    {
        Destroy(_currentPins);
        _currentPins = Instantiate(_pins, transform.position, transform.rotation);
        _signalReceived = false;
    }
}
