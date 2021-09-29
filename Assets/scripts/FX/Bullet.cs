using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeUntilDestroy;

    [Space(10)]
    public int demage;

    public GameObject impactPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<TerrainCollider>())
        {
            SpawnBulletInpact(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("barrelStorage"))
        {
            SpawnBulletInpact(coll.gameObject);
            coll.gameObject.GetComponent<BarrelStorage>().SubtractDemage(demage);
        }
    }

    void SpawnBulletInpact(GameObject coll)
    {
        Instantiate(impactPrefab, coll.GetComponent<Collider>().ClosestPoint(transform.position), Quaternion.identity);
    }
}
