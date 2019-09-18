using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnLookat : MonoBehaviour
{
    private Transform target;
    public bool alwaysRotate;

    private void Start()
    {
        target = GameObject.FindObjectOfType<PlayerFlyController>().transform;
    }

    private void Update()
    {
        if (alwaysRotate == true)
        {
            RotateToPlayer();
        }
    }

    public void RotateToPlayer()
    {
        transform.LookAt(target);
    }

}
