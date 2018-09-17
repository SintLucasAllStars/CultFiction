using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBob : MonoBehaviour
{
    private Vector3 originalCamPos;

    void Start ()
    {
        originalCamPos = transform.position;
    }

    private void Update()
    {
        transform.position = originalCamPos + new Vector3(0, (float)System.Math.Sin(Time.fixedTime) * 0.05f, 0);
    }
}
