using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Syringe : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _hit = false;

    public float Speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
     

    }

    private void OnCollisionEnter(Collision other)
    {
        if (_hit)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            GameManager.Instance.GetNewSyringe();
           
         
            Destroy(this);
       
        }
   


    }

    // Update is called once per frame
	void Update () 
	{
	    if (Input.GetMouseButtonDown(0) && !_hit)
	    {
	        transform.SetParent(null);
	        _rigidbody.useGravity = true;
	        _rigidbody.AddForce(-transform.forward * Speed, ForceMode.Impulse);
	        _hit = true;
	
	    }
	}

  
}
