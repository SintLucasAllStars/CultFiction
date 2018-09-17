using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
    public Vector3 target;
    public float speed;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Walktarget").transform.position;
        target.x += 2f;
    }
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;
        // get the angle
        Vector3 norTar = (target - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, target.y, transform.position.z), step);
        if(transform.position.x == target.x)
        {
            Destroy(gameObject);
        }
    }
}
