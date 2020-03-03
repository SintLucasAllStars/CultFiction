using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour

{
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public AudioSource JumpSound;
    private bool facing = true;
    private float switching;

    // Start is called before the first frame update
    void Start()
    {
         JumpSound = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        switching = Input.GetAxis ("Horizontal");
        if (switching > 0 && !facing) 
		{
			Flip ();
		}
		else if (switching < 0 && facing) 
		{
			Flip ();
		}
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && isGrounded == true){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            JumpSound.Play();
        }
    }
    
    private void Flip ()
	{
		facing = !facing;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
