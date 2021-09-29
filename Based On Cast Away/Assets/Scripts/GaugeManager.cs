using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    public GameObject pin;
    public GameObject van;
    public float km_h;
    public float angle;

    // Start is called before the first frame update
    void Start()
    { 

    }

    // get current speed form car and apply to rotation of the gaugemeter pin
    void Update()
    {
        km_h = van.GetComponent<CarControl>().KM_H;
        angle = -km_h - -130;
        pin.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Debug.Log(km_h);
    }
}
