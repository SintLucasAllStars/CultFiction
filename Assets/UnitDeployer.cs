using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeployer : Enemy {
    public WaveSpawner ws;
    private bool isDeploying = false;
    public int DeployingInterfal;
	// Use this for initialization
	void Start () {
        ws = FindObjectOfType<WaveSpawner>();

	}
	
	// Update is called once per frame
	void Update () {
        MoveToWards(new Vector3(-8, 0, 0));
	}
    public override void MoveToWards(Vector3 target)
    {
        base.MoveToWards(target);
        if (!isDeploying)
        {
            if (transform.position.x == target.x)
            {
                StartCoroutine(DeployingUnits(DeployingInterfal));
                isDeploying = true;
            }
        }
        
    }

    public IEnumerator DeployingUnits(int sec)
    {
        while (true)
        {

        ws.SpawnGroundUnit(new Vector3(transform.position.x, -1.7f,0),0);
        yield return new WaitForSeconds(sec);
        }
    }
}
