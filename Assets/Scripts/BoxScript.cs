using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxScript : MonoBehaviour
{

    public Item item;
    public int hp;
    BoxManager bm;
    AudioSource asc;

    // Use this for initialization
    void Start()
    {
        asc = FindObjectOfType<AudioSource>();
        bm = FindObjectOfType<BoxManager>();
        this.gameObject.name = "Box (" + item.name + ")";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f || transform.position.y > 10f)
        {
            Destroyed();
        }
    }

    void Destroyed()
    {
        Destroy(GetComponent<FloatScript>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<Collider>());

        this.transform.rotation = Quaternion.identity;
        float height = (bm.Floor.transform.lossyScale.y / 2f) + .3f;
        //bm.Floor.transform.position.y + 1f
        this.transform.position = new Vector3(transform.position.x, height, transform.position.z);

        GameObject pan = Instantiate(bm.panel, transform.position, Quaternion.identity, transform);
        pan.GetComponent<Renderer>().material.mainTexture = item.image;


        iTween.MoveTo(pan, new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z), 2f);
        iTween.RotateTo(pan, new Vector3(90f, 0f, 0f), 1f);

        bm.boxes.Remove(this.gameObject);
        bm.CheckBoxes();

        CoconutManager.score += item.value;
        Destroy(this.gameObject, 3f);
    }


    void TakeDamage(Vector3 v)
    {
        if (v.z < 0) v.z = -v.z;
        int damage = (int)(v.z / 2f);
        hp -= damage;
        if (hp <= 0)
        {
            Destroyed();
            return;
        }
        Debug.Log("Damage: " + damage + " / " + hp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Nut"))
        {
            TakeDamage(collision.relativeVelocity);
            if (asc != null)
            asc.Play();
        }
    }
}