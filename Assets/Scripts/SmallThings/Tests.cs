using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for(int i = 0; i <5; i++)
        {
            Debug.Log(Random.value * 100);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
