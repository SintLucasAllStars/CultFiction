using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_3 : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        gameObject.transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }
}
