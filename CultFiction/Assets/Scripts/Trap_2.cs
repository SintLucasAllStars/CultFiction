using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_2 : MonoBehaviour
{
    private float timer;
    public float resetTimer;

    public float spikeSpeed;

    public bool spikeActivate;
    public bool active;

    public GameObject spikes;

    private void Start()
    {
        timer = 0.0f;

        spikeActivate = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (spikeActivate || active)
        {
            spikes.transform.localPosition = Vector3.Lerp(spikes.transform.localPosition, new Vector3(0.0f, 5.0f, 0.0f), spikeSpeed);
        }
        else if (!active)
        {
            spikes.transform.localPosition = Vector3.Lerp(spikes.transform.localPosition, new Vector3(0.0f, 0.0f, 0.0f), spikeSpeed);
        }

        if (timer < resetTimer * 0.8)
        {
            spikeActivate = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Clickable"))
        {
            if (timer < 0.0f)
            {
                spikeActivate = true;

                timer = resetTimer;
            }
        }
    }
}
