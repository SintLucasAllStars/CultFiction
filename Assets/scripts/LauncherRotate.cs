using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherRotate : MonoBehaviour
{

    KeyCode leftKey = KeyCode.A;
    KeyCode rightKey = KeyCode.D;
    public float roSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(leftKey))
        {
            transform.Rotate(0, -roSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(rightKey))
        {
            transform.Rotate(0, roSpeed * Time.deltaTime, 0);
        }
    }
}
