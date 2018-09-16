using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    public Camera cam;
    public GameManager manager;

    void Update ()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out hit) && manager.gameRunning)
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.GetComponent<Iinteractable>().OnClick();
                }
            }

        }

	}
}
