using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTie : MonoBehaviour
{
    public GameObject tie;
    public bool canSpawn;
    public bool noTie;
    private void Start()
    {
        StartCoroutine(waiter());
        noTie = true;
    }

    private void Update()
    {
        if (canSpawn&& noTie)
        {  
            Instantiate(tie, new Vector3(Random.Range(-100, 100), Random.Range(0, 0), Random.Range(-100, 100)), Quaternion.identity);

            canSpawn = false;
            StartCoroutine(waiter());
            
        } 
    }
    
    IEnumerator waiter()
    {
        int wait_time = Random.Range (20, 40);
        yield return new WaitForSeconds (wait_time);
        canSpawn = true;
    }
    

    
}