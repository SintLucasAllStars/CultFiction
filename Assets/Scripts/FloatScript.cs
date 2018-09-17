using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatScript : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPos;
    Vector3 floatpos;
    float halfHeight;

    public bool debug = false;

    public bool floating = false;

    bool uphit;
    bool downhit;

    private void Start()
    {
        halfHeight = transform.lossyScale.y / 2f;
        rb = GetComponent<Rigidbody>();
    }

    public bool isFloating() { return floating; }

    private void FixedUpdate()
    {
        startPos = transform.position;
        //startPos.y += halfHeight * 2f;
        floatpos = transform.position;
        // floatpos.y += -halfHeight;

        RaycastHit hitUp;
        RaycastHit hitDown;

        if (debug) Debug.DrawRay(startPos, Vector3.up, Color.red);
        if (debug) Debug.DrawRay(floatpos, -Vector3.up, Color.blue);

        if (Physics.Raycast(transform.position, Vector3.up, out hitUp))
        {
            if (hitUp.transform.gameObject.CompareTag("Water"))
            {
                uphit = true;
                rb.AddForce(Vector3.up * (hitUp.distance / 2f), ForceMode.Impulse);
            }
            else uphit = false;
        }

        if (Physics.Raycast(startPos, transform.up, out hitDown, -(halfHeight * 3f)))
        {
            if (hitDown.transform.gameObject.CompareTag("Water") && rb.velocity.y < 3f)
            {
                downhit = true;
                rb.AddForce(Vector3.up * .2f, ForceMode.Impulse);
            }
            else downhit = false;
        }
        else downhit = false;

        SetFloating();
    }

    void SetFloating()
    {
        if (downhit || uphit)
            floating = true;
        else floating = false;
    }
}
