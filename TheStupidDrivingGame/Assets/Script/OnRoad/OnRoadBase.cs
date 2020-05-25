using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnRoadBase : MonoBehaviour
{
    [Header("Movement settings")]
    public float maxSensitivity = 12;
    public float minSensitivity = 6;

    protected float sensitivity;

    protected virtual void Start()
    {
        sensitivity = Random.Range(minSensitivity, maxSensitivity);
    }

    protected virtual void OnRoadMovement()
    {
        transform.Translate(Vector3.back / sensitivity);

        if (transform.position.z < 1f)
            Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            OnCarHit();
        }
    }

    protected abstract void OnCarHit();
}
