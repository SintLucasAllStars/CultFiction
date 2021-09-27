using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        transform.rotation = Quaternion.Euler(-mousePos.y/100, 40+mousePos.x/100, 0f);
    }
}
