using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Transform thirdPersonPos;
    public Transform firstPersonPos;

    private Camera cam;

    //[HideInInspector]
    public bool thirdPersonView;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamPos();
        }
    }

    public void SwitchCamPos()
    {
        thirdPersonView = !thirdPersonView;

        if (thirdPersonView)
        {
            cam.transform.position = thirdPersonPos.transform.position;
        }
        else if (!thirdPersonView)
        {
            cam.transform.position = firstPersonPos.position;
        }
    }

}
