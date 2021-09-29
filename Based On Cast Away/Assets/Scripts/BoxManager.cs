using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject playerVan;
    public int boxcount;
    // Start is called before the first frame update
    void Start()
    {
        boxcount = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(boxcount);
        if(playerVan.GetComponent<CarControl>().box == true)
        {
            boxcount++;
            playerVan.GetComponent<CarControl>().box = false;
        }
    }
}
