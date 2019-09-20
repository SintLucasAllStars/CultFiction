using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class RayCaster : MonoBehaviour
{

    public Camera cam;
    RaycastHit hit;
    public GameObject door;
    public bool keyGet = false;

    public Toggle toggle;
    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Key"))
            {
                keyGet = true;
                toggle.onoff();
                hit.collider.gameObject.SetActive(false);
            }
            if (hit.collider.CompareTag("Door") && keyGet == true)
            {
                door.transform.localEulerAngles = new Vector3(0, -100, 0);
                toggle.onoff();
            }
        }
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Key"))
        {
            toggle.LookKeyON();
        }
        if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Key") && keyGet == false)
        {
            toggle.LookKeyOFF();
        }
    }
}
