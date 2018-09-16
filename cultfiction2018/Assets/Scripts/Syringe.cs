using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Syringe : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _hit = false;
    private bool _triggerd;

    public float Speed;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

    }

    private void FixedUpdate()
    {
     

    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerd = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_hit)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            _rigidbody.useGravity = false;
            GameManager.Instance.GetNewSyringe();

            if (other.gameObject.CompareTag("TheBody"))
            {

                if (!_triggerd)
                {
                    GameManager.Instance.BodyHit(other.contacts[0].point);
                }
                SoundManager.Instance.PlaySyringeSounds();
                SoundManager.Instance.PlayBloodSounds();

            }
            else
            {
                SoundManager.Instance.PlaySyringeHitSound();
            }
           
            
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
