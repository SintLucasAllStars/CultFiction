using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public GameObject fireBallPrefab;

    private void Start()
    {
        StartCoroutine("StartShooting");
    }
    //Co routine 
    IEnumerator StartShooting()
    {
        for (int i = 0; i < 100; i++)
        {
            int seconds = RandomSecond();
            InstantiateFireBalls();
            yield return new WaitForSeconds(seconds);
        }
    }
    // making use of a function that requires a return
    int RandomSecond()
    {
        int seconds;
        seconds = Random.Range(8,15);
        return seconds;
    }
    // instantiating new items in the scene at a random position
    void InstantiateFireBalls()
    {
        Vector3 gennedPosition = new Vector3(Random.Range(30, 30), Random.Range(150,250), Random.Range(30, 30));
        Instantiate(fireBallPrefab, gennedPosition, Quaternion.identity);
    }




}
