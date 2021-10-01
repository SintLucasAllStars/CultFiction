using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timeUntilDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForDestroy(timeUntilDestroy));

    }

    IEnumerator WaitForDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
