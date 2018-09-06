using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    public Item item;
    public int hp;
    ItemList il;

    // Use this for initialization
    void Start()
    {
        il = FindObjectOfType<ItemList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f)
        {
            Destroyed();
        }
    }

    void Destroyed()
    {
        Destroy(GetComponent<Rigidbody>());
        //iTween.rot(this.gameObject, Quaternion.identity.eulerAngles, 1f);

        this.transform.rotation = Quaternion.identity;
        this.transform.position = new Vector3(transform.position.x, il.Floor.transform.position.y + 1f, transform.position.z);
        //iTween.MoveTo(this.gameObject, new Vector3(transform.position.x, il.Floor.transform.position.y + 1f, transform.position.z), 2f);

        GameObject pan = Instantiate(il.panel, transform.position, Quaternion.identity, transform);
        pan.GetComponent<Renderer>().material.mainTexture = item.image;
        iTween.MoveTo(pan, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), 2f);
        iTween.RotateTo(pan, new Vector3(-90f, 0f, 0f), 1f);

        Destroy(this.gameObject, 3f);
    }

    void TakeDamage(Vector3 velocity)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Nut"))
        {
            TakeDamage(collision.relativeVelocity);
        }
    }
}
