using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void FixedUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {

        if (offsetPositionSpace == Space.Self)
        {
            transform.position = Vector3.Lerp(transform.position,target.TransformPoint(offsetPosition),0.1f);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}