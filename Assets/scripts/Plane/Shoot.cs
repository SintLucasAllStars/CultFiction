using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gunShotFX;
    public Transform rightGun, leftGun;

    private bool shotsFired = false;

    [Space(10)]
    public float timeBetweenShots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") != 0 && !shotsFired)
        {
            StartCoroutine(FireGuns(timeBetweenShots * Time.deltaTime));
            shotsFired = true;
        }
    }

    IEnumerator FireGuns(float t)
    {
        Instantiate(bullet, leftGun);
        Instantiate(bullet, rightGun);
        Instantiate(gunShotFX, leftGun);
        Instantiate(gunShotFX, rightGun);
        yield return new WaitForSeconds(t);
        shotsFired = false;
    }
}
