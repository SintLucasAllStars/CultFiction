using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float baseSpeed;
    public float maxSpeed;
    public float speedMultiplier;
    public float totalSpeed;

    [Header("Rotation")]
    public float baseRotationSpeed;
    public float maxRotationSpeed;
    public float rotationMultiplier;
    public float totalRotationSpeed;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        //making sure the plane moves at least as fast as the baseSpeeds.
        totalSpeed = baseSpeed;
        totalRotationSpeed = baseRotationSpeed;
        initialRotation = CloneQuaternion(transform.rotation);
        targetRotation = CloneQuaternion(initialRotation);
    }

    // Update is called once per frame
    void Update()
    {
        Step();
        Rotate();
    }

    void Step()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //making the plane move gradually faster over time
            totalSpeed += speedMultiplier * Time.deltaTime;
            totalSpeed = Mathf.Clamp(totalSpeed, baseSpeed, maxSpeed);
            transform.Translate(0, 0, totalSpeed * Time.deltaTime);
        }
        else
        {
            //making the plane move gradually slower over time
            totalSpeed -= (speedMultiplier * 2) * Time.deltaTime;
            totalSpeed = Mathf.Clamp(totalSpeed, baseSpeed, totalSpeed);
            transform.Translate(0, 0, totalSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            targetRotation *= Quaternion.AngleAxis(totalRotationSpeed, transform.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            targetRotation *= Quaternion.AngleAxis(-totalRotationSpeed, transform.right);
        }
        if (Input.GetKey(KeyCode.D))
        {            
            targetRotation *= Quaternion.AngleAxis(-totalRotationSpeed, transform.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            targetRotation *= Quaternion.AngleAxis(totalRotationSpeed, transform.forward);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            targetRotation = CloneQuaternion(initialRotation);
        }

        //print(totalSpeed);
    }

    public Quaternion CloneQuaternion(Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }

    void Rotate()
    {
        float amount = totalRotationSpeed * (Quaternion.Angle(transform.rotation, targetRotation) / 30.0f);
        amount = Mathf.Min(amount, totalRotationSpeed*2f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, amount);
    }
}
