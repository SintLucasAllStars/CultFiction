using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebbelBomber : Enemy {
    private Vector3 target;
    public GameController gc;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Walktarget").transform.position;
        gc = FindObjectOfType<GameController>();
        speed = Random.Range(2, speed);
        target += new Vector3(2f, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        MoveToWards(target); 
    }
    public override void MoveToWards(Vector3 target)
    {
        base.MoveToWards(target);
        if(target.x == transform.position.x)
        {
            gc.TakeDamage(damage);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            gc.SpawnAnimation(pos, 0);
            IsDeath();      
        }
    }
}
