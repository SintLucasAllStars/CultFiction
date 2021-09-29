using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    float despawnTimer;
    public float seconds;

    void Start()
    {
        
    }

    void Update()
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= seconds)
        {
            Destroy(this.gameObject);
        }
    }
}
