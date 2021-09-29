using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeUntilDestroy;

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
            Instantiate(impactPrefab, coll.collider.ClosestPoint(transform.position), Quaternion.identity);
            //Destroy(this.gameObject, 0.2f);
        }
    }
}
