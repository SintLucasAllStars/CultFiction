using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;

    private void Awake()
    {
        StartCoroutine("DestroyDelay");
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Do Damage
        if(collision.gameObject.GetComponent<AIController>())
        {
            //Do Damage to AI
            collision.gameObject.GetComponent<AIController>().DoDamage(damage);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 250, ForceMode.VelocityChange);

        }
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().DoDamage(damage);
        }

        Destroy(this.gameObject);
    }

    IEnumerator DestroyDelay()
    {

        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);

    }
}
