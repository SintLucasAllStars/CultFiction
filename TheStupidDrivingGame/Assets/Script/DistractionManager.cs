using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionManager : MonoBehaviour
{
    public Transform left, right, up, down, front, back;
    
    [Space(5)]
    public GameObject foodPrefab;

    private Vector3 GetRandomBorders()
    {
        return new Vector3(
            Random.Range(left.position.x, right.position.x),
            Random.Range(down.position.y, up.position.y),
            Random.Range(back.position.z, back.position.z));
    }

    public void SpawnFood()
    {
        Instantiate(foodPrefab, GetRandomBorders(), Quaternion.identity);
    }
}
