using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;
    private void FixedUpdate()
    {
        transform.Rotate(xRot,yRot,zRot);
    }
}
