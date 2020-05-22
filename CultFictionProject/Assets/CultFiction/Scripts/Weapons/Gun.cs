using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Gun : MonoBehaviour
{
    [SerializeField]
   

    public VisualEffect Muzzle;

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject MuzzleLoc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        var go = Instantiate(Bullet, MuzzleLoc.transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.right * 2000);
        Muzzle.Play();
    }
}
