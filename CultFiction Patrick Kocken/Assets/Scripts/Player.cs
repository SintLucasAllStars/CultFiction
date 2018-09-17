using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public void WalkStep(Vector3 nodeWorldPos){
        StartCoroutine(Move(nodeWorldPos));
    }

    public IEnumerator Move(Vector3 nodeWorldPos)
    {
        float start = Time.time;
        float elapsed = 0;

        Vector3 targetDir = nodeWorldPos - transform.position;

        while (elapsed < 1)
        {
            elapsed = Time.time - start;

            //Rotation
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, elapsed / 1, -0));

            //Position
            transform.position = Vector3.MoveTowards(transform.position, nodeWorldPos, elapsed / 1);

            yield return null;
        }
    }
     private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Animation>().Play();
            GameController.instance.EndGame();
            Destroy(this);
        }

    }
}
