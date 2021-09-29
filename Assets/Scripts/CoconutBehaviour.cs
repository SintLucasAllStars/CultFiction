using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutBehaviour : MonoBehaviour
{

    public GameObject brokenCoconut;
    public CoconutSpawner coconutSpawner;
    private bool isHit;
    public bool isBroken;
    private Vector3 position;
    private Rigidbody rb;
    private AudioSource thudSound;

    void Start()
    {
        isHit = false;
        isBroken = false;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(CoconutSpawner.mainCamera.transform.forward*Random.Range(160f, 280f));
        rb.AddForce(transform.up*Random.Range(140f, 220f));
        rb.AddForce(transform.right *Random.Range(-50f,50f));
        thudSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && isHit == false)
        {
            int randomBreak;
            randomBreak = Random.Range(0, 6);
            isHit = true;
            thudSound.Play();

            if (randomBreak >= 3 && isBroken == false)
            {
                position = this.transform.position;
                Instantiate(brokenCoconut, position, Quaternion.identity);
                isBroken = true;
                coconutSpawner.AddCoconutCount();
                Destroy(this.gameObject);
            }   
        }
    }
}
