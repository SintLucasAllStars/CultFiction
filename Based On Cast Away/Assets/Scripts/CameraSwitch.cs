using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Transform thirdPersonPos;
    public Transform firstPersonPos;
    public GameObject insideGauge;
    public GameObject Gauge;

    private Camera cam;

    //[HideInInspector]
    public bool thirdPersonView;
    // Start is called before the first frame update
    void Start()
    {
        Gauge.SetActive(false);
        cam = Camera.main;
        cam.transform.Rotate(new Vector3(-20, 0, 0));
        cam.transform.position = firstPersonPos.position;
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
            cam.transform.Rotate(new Vector3 (20, 0, 0));
            insideGauge.SetActive(false);
            Gauge.SetActive(true);
        }
        else if (!thirdPersonView)
        {
            cam.transform.position = firstPersonPos.position;
            cam.transform.Rotate(new Vector3(-20, 0, 0));
            insideGauge.SetActive(true);
            Gauge.SetActive(false);
        }
    }

}
