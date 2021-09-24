using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    float despawnTimer;

    void Start()
    {
        
    }

    void Update()
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= 5f)
        {
            Destroy(this.gameObject);
        }
    }
}
