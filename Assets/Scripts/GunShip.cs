using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShip : MonoBehaviour {
    [HideInInspector]
    public WaveSpawner sm;
    [HideInInspector]
    public PlayerManager pm;
    [HideInInspector]
    public GameController gc;

    public float speed;
    Vector3 TargetPoint;
    bool destroyed = false;

    void Start () {
        sm = FindObjectOfType<WaveSpawner>();
        pm = FindObjectOfType<PlayerManager>();
        gc = FindObjectOfType<GameController>();
        TargetPoint = sm.GenerateMovePoint();

        speed = Random.Range(4, speed);
    }

	void Update () {
        if(!destroyed)
        MoveTowardPoint();

	}
    private void MoveTowardPoint()
    {
        if(transform.position == TargetPoint)
        {
            TargetPoint = sm.GenerateMovePoint();
        }
        //move to random point 
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPoint, step);

        //rotates to the target 
        Vector3 dif = TargetPoint - transform.position;
        dif.Normalize();

        float rot_z = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }
    public void KillAirFighter(Vector3 mousepos)
    {
        destroyed = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        AddExplosionForce(300, mousepos, 20);
        rb.gravityScale = 1.5f;
        bc.enabled = true;

    }
    public void AddExplosionForce(float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {  
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            ContactPoint2D contact = collision.contacts[0];
            gc.SpawnAnimation(contact.point,0);
            sm.KillGroundTroops(contact.point, 1.5f,50);
            sm.airFighters.Remove(this.gameObject);
            Destroy(this.gameObject);

        }
    }
}
