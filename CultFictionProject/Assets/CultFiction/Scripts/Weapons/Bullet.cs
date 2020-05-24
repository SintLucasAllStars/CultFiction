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
        if(collision.gameObject)
        {
            //Do Damage to AI
            

        }

        Destroy(this.gameObject);
    }

    IEnumerator DestroyDelay()
    {

        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);

    }
}
