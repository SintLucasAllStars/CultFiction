using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationShipScript : MonoBehaviour
{
    private float rotationSpeed = 30f;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
