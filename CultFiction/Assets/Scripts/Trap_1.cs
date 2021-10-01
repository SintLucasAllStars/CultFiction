using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_1 : MonoBehaviour
{
    private float timer;
    public float resetTimer;

    public Transform arrowDispenser;

    public GameObject arrow;

    private void Start()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Clickable"))
        {
            if (timer < 0.0f)
            {
                Instantiate(arrow, arrowDispenser.transform.position, Quaternion.Euler(gameObject.transform.rotation.eulerAngles));

                timer = resetTimer;
            }
        }
    }
}
