using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatforms : MonoBehaviour
{

    public float speed;
    public float maxRotation = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Rotate(Vector3.up * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler( 0f, maxRotation * Mathf.Sin(Time.time * speed), 0f);
    }
}
