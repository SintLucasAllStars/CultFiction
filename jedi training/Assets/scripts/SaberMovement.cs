using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberMovement : MonoBehaviour
{
    bool slicing = false;
    public Transform hitBox;

    public int sliceSpeed;
    public float rotSpeed;
    int rotated = 0;
    bool slicingBack = false;
    public Transform cameraRig;
    public Transform saberHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            slicing = true;
        }

        if (slicing && !slicingBack && rotated < 180)
        {
            transform.Rotate(0, -sliceSpeed, 0);
            rotated += sliceSpeed;

        } else if (slicing && !slicingBack && rotated >= 180)
        {
            slicing = false;
            slicingBack = true;
            
        }

        if (slicing && slicingBack && rotated > 0)
        {
            transform.Rotate(0, sliceSpeed, 0);
            rotated -= sliceSpeed;

        }
        else if (slicing && slicingBack && rotated <= 0)
        {
            slicing = false;
            slicingBack = false;
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !slicing)
        {
            transform.Rotate(0, 0, rotSpeed);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !slicing)
        {
            transform.Rotate(0, 0, -rotSpeed);
        }

        saberHolder.rotation = cameraRig.rotation;

        if (slicingBack)
        {
            hitBox.eulerAngles = new Vector3(hitBox.eulerAngles.x, hitBox.eulerAngles.y, -transform.eulerAngles.z);
        }
        else
        {
            hitBox.eulerAngles = new Vector3(hitBox.eulerAngles.x, hitBox.eulerAngles.y, transform.eulerAngles.z);
        }

    }
}
