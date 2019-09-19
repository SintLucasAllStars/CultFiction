using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{

    public float delta = 3.5f;  // Amount to move left and right from the start point
    public float speed;// = Random.Range(0,3f);
    private Vector3 startPos;

    public GameObject stormTrooper;
    public List<GameObject> enemys;

    void Start()
    {
        StartCoroutine(Spawner());
        speed = Random.Range(0, 3f);
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        Instantiate(enemys[Random.Range(0, enemys.Count)], this.transform.position, Quaternion.identity);
        StartCoroutine(Spawner());
    }
}