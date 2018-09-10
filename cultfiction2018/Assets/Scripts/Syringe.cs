using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Syringe : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _hit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        if (_rigidbody.useGravity && _hit)
        {

        }

    }

    private void OnCollisionEnter(Collision other)
    {
        _hit = false;;
    }

    // Update is called once per frame
	void Update () 
	{
	    if (Input.GetMouseButtonDown(0))
	    {
	        transform.SetParent(null);
	        _rigidbody.useGravity = true;
	        _rigidbody.AddRelativeForce(0,2000,0);
	    }
	}
}
