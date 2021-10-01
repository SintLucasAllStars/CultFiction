using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public float movementSpeed;

    private void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.tag);

        if (!col.gameObject.CompareTag("Trap"))
        {
            movementSpeed = 0.0f;

            gameObject.GetComponent<Collider>().enabled = false;
        }

        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Clickable"))
        {
            gameObject.transform.parent = col.gameObject.transform;

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
