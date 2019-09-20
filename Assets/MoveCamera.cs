using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float val = Mathf.Sin(Time.realtimeSinceStartup) * 1 + 1;
        transform.position = initialPos + new Vector3(0.0f, val, val);
    }
}
