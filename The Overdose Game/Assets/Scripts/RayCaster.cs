using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update ()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    hit.collider.GetComponent<Iinteractable>().OnClick();
                }
            }

        }
	}
}
