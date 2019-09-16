using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnLookat : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindObjectOfType<PlayerFlyController>().transform;
    }
    
    public void RotateToPlayer()
    {
        transform.LookAt(target);
    }

}
