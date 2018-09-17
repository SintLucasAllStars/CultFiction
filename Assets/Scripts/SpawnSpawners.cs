using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpawners : MonoBehaviour
{

    [SerializeField] int totalSpawners;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] Vector2 rangeX;
    [SerializeField] Vector2 rangeZ;
    [SerializeField] float height;

    [SerializeField] LayerMask layer;

    // Use this for initialization
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 1; i <= totalSpawners; i++)
        {
            Instantiate(spawnPrefab, GetPoint(), Quaternion.identity, transform).GetComponent<Spawner>().Initialize(player);
        }
    }

    Vector3 GetPoint()
    {
        RaycastHit hit;
        Vector3 startpoint = new Vector3(GetRange(rangeX), height, GetRange(rangeZ));
        if(Physics.Raycast(startpoint, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            return hit.point;
        }
        else
        {
            return GetPoint();
        }
    }

    float GetRange(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
}
