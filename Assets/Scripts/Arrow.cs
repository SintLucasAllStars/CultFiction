using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public DirectionsEnum _direction;

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
    }
}
