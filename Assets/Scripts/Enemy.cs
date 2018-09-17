using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public string enemyName;
    public float health;
    public int killPoints;
    public float speed;
    public float damage;
    public int spawnPrecentage;
    public bool isDeath = false;

    public virtual void MoveToWards(Vector3 target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, transform.position.z), step);
    }
    public static Vector3 GenerateRandomTargetPoint(Vector3 target)
    {
        float Xoffset = Random.Range(0, 8f);
        return new Vector3(target.x - Xoffset, 0, 0);
    }
    public void IsDeath()
    {
        gameObject.SetActive(false);
        isDeath = true;
        
    }
    public void DestroyPrefab(int secToDestroy)
    {
        Destroy(gameObject, secToDestroy);
    }

}
