using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool blockInput;
    public int camAngle;

    void Start()
    {
        blockInput = true;
        camAngle = 45;
    }

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        if (!blockInput)
        {
            Vector3 mousePos = Input.mousePosition;
            transform.eulerAngles = new Vector3(-mousePos.y / 100, camAngle + mousePos.x / 100, 0f);
        }
    }
}
