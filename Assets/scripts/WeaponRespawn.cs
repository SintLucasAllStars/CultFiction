using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRespawn : MonoBehaviour
{
    float timer;
    int waitingTime = 3;
    bool canReload = true;

    public GameObject Respawner;
    public GameObject Weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            Debug.Log("reloaded");
            canReload = true;
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canReload == true)
        {
            Instantiate(Weapon);
            Debug.Log("created");
            timer = 0;
            canReload = false;
        }
    }
}
