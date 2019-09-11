using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRocket : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    
    [SerializeField]
    [Range(0, 2)]
    private float rotationSpeed;
    
    [SerializeField] private float focusDistance = 5;
    public Transform target;
    private bool isFollowingTarget = true;
    private Vector3 tempVector;

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < focusDistance)
        {
            isFollowingTarget = false;
        }

        Vector3 targetDirection = target.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward , targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        MoveForward(Time.deltaTime);

        if (isFollowingTarget)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }
    
    private void MoveForward(float rate)
    {
        transform.Translate(Vector3.forward * rate * speed, Space.Self);
    }

}
