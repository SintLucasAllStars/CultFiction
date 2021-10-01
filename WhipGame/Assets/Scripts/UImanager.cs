using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject deadthUI;
    private Health hp;

    private void Start()
    {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    void Update()
    {
        if (hp.Dead == true) { deadthUI.gameObject.SetActive(true); mainUI.gameObject.SetActive(false); }
        else if(hp.Dead == false) deadthUI.gameObject.SetActive(false); mainUI.gameObject.SetActive(true); ;
    }
}
