using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform head; 

    // Update is called once per frame
    void Update()
    {
        transform.position = head.transform.position; 
    }
}
