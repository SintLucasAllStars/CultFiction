using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float mouseSencitivity;
    public Transform spine;
    public Transform rig;
    public float maxRot;
    public float minRot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float rY = Input.GetAxis("Mouse X") * mouseSencitivity;
        float rX = Input.GetAxis("Mouse Y") * mouseSencitivity;

        transform.Rotate(0,rY,0);
        rig.Rotate(rX, 0, 0);

        

        if (rig.eulerAngles.x > maxRot && rig.eulerAngles.x < minRot)
        {
            if (rig.eulerAngles.x < 180)
            {
                
                rig.eulerAngles = new Vector3(maxRot, rig.eulerAngles.y);
                
            }
            else
            {
                rig.eulerAngles = new Vector3(minRot, rig.eulerAngles.y);
                


            }
            
        }

        //spine.rotation = rig.rotation;
        

    }
}
