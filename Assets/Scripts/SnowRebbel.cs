using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowRebbel : MonoBehaviour
{
    [HideInInspector]
    public GameController gm;
    [HideInInspector]
    public SpawnManager sp;
    public GameObject lazer;
    public Transform target;
    public float speed;
    public int destroyedpoints;

    Vector3 targetpos;
    bool active = false;

    void Start()
    {
        gm = FindObjectOfType<GameController>();
        sp = FindObjectOfType<SpawnManager>();
        speed = Random.Range(1, speed);
        target = GameObject.FindGameObjectWithTag("Walktarget").transform;
        GenerateRandomTargetPoint();
    }
    void Update()
    {

        if (!active)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetpos.x, transform.position.y, transform.position.z), step);

            if (transform.position.x == targetpos.x)
            {
                StartCoroutine(ShootAtAt());
                active = true;
            }

        }





    }
    void GenerateRandomTargetPoint()
    {
        float Xoffset = Random.Range(0, 8f);
        targetpos = new Vector3(target.position.x - Xoffset, 0, 0);
    }
    IEnumerator ShootAtAt()
    {
        while (true)
        {
            gm.TakeDamage(sp.ReturnTrooper(gameObject).damage);
            if(lazer != null)
            {
            Instantiate(lazer, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3);
        }

    }
}
