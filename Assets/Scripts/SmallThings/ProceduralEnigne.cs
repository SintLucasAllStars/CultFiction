using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnigne : Singleton<ProceduralEnigne> {
    public int seed = 1024;

	void Awake () {
        DontDestroyOnLoad(gameObject);
		seed = Random.Range (0, 1024);
        SetSeed(seed);
    
	}
	public void SetSeed(int value)
    {
        seed = value;
        Random.InitState(seed);
    }
}
