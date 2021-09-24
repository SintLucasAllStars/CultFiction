using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBehaviour : MonoBehaviour
{

    public GameObject brokenCoconut;
    private bool isBroken;
    private Vector3 position;

    void Start()
    {
        isBroken = false;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken == false)
        {
            position = this.transform.position;
            Instantiate(brokenCoconut, position, Quaternion.identity);
            isBroken = true;
            Destroy(this.gameObject);
        }
    }
}
