using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRaise : MonoBehaviour
{
    private GameObject text;

    private void Start()
    {
        StartCoroutine("Raise");
        text = GameObject.FindGameObjectWithTag("warning");
    }

    IEnumerator Raise()
    {
        for (int i = 0; i < 100; i++)
        {
            print("Lava up");
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            StartCoroutine("Warning");
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator Warning()
    {
        for (int i = 0; i < 3; i++)
        {
            text.SetActive(true);
            yield return new WaitForSeconds(1);
            text.SetActive(false);
        }
    }
}
