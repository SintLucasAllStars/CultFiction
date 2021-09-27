using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    public GameObject pin;
    public GameObject carcontrol;
    public int km_h;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle = 130;
        carcontrol.GetComponent<CarControl>().KM_H = km_h;
    }

    // Update is called once per frame
    void Update()
    {
        //angle = km_h - 130;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //if (km_h <= 0)
        //{
        //    angle = 130;
        //}

        

        //if (km_h >= 190)
        //{
        //    angle = -130;
        //}

    }
}
