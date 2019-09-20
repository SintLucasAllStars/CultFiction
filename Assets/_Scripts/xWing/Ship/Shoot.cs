using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform[] shootPos;

    private GameObject oldBullet;

    public int bulletCount = 0;
    
    public AudioClip laser;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(wait());
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(laser, 0.3F);
            for (int i = 0; i < shootPos.Length; i++)
            { 
                Rigidbody clone;
                clone = bullet.GetComponent<Rigidbody>();
                clone = Instantiate(bullet, shootPos[i].position, transform.rotation);
                clone.velocity = transform.TransformDirection(Vector3.forward * 1000);
                
                bulletCount++;

            }

            
        }
        
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        DestroyAll();
    }

    void DestroyAll()
    {
        StartCoroutine(wait());
        for (int i = 0; i < bulletCount; i++)
        {
            oldBullet = GameObject.FindWithTag("PlayerBullet");
            DestroyImmediate(oldBullet);
            bulletCount --;
        }
       
    }
}
