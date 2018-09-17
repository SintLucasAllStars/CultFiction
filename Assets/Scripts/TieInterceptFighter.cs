using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieInterceptFighter : EnemieController
{
    private ScoreManager scoreManager;

    public int scoreValue;

    public void Start()
    {
        GameObject scoreManagerObject = GameObject.FindWithTag("GameManager");
        if(scoreManagerObject != null)
        {
            scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        }
        if(scoreManager == null)
        {
            Debug.Log("cant find ScoreManager script");
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Test");
            scoreManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
