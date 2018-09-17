using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundUnit : Enemy
{
    private Vector3 target;
    public GameObject lazer;
    public GameController gc;
    private bool isShooting = false;
    // Use this for initialization
    void Start()
    {
        target = GenerateRandomTargetPoint(GameObject.FindGameObjectWithTag("Walktarget").transform.position);
        gc = FindObjectOfType<GameController>();
        speed = Random.Range(0.8f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWards(target);
    }
    public override void MoveToWards(Vector3 target)
    {
        base.MoveToWards(target);

        if (!isShooting)
        {
            if (transform.position.x == target.x)
            {
                StartCoroutine(ShootAtAt());
                isShooting = true;
            }
        }
    }
    IEnumerator ShootAtAt()
    {
        while (true)
        {
            gc.TakeDamage(damage);
            if (lazer != null)
            {
                Instantiate(lazer, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(3);
        }

    }
}
