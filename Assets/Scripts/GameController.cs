using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Slider healthBar;
    public GameObject[] animation;
    [HideInInspector]
    public ScoreManager sm;

    // Use this for initialization
    void Start () {
        sm = FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(float damage)
    {
        healthBar.value = healthBar.value - (damage * 0.01f); 

        if(healthBar.value == 0)
        {
            sm.GameOver();
        }
    }
    public void SpawnAnimation( Vector2 pos,int indexAnimation)
    {
        GameObject explos = Instantiate(animation[indexAnimation], new Vector3(pos.x,pos.y,0), Quaternion.identity);    
        Destroy(explos, explos.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
