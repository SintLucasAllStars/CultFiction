using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterCameraScript : MonoBehaviour
{

    public GameObject rotatePoint;
    public Vector3 AddRotation;
    public float time;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Camera.main.gameObject.transform.LookAt(rotatePoint.transform);
        iTween.RotateAdd(rotatePoint, AddRotation, time);
    }
}