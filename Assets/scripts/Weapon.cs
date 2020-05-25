using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Ball;
    public float launchForce = 1000;
    public Rigidbody rigidbody;
    bool hasShot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasShot == false)
        {
            hasShot = true;
            rigidbody.AddForce(launchForce * transform.forward);
            Destroy(this.gameObject, 9);    
        }
    }
}
