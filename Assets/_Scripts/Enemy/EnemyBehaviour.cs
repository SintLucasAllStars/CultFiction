using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Ship
{
    public GameObject lockOn;
    public GameObject mapIndicator;
    public bool dead;

    public LockOnLookat rotateToPlayer;
    
    void Update()
    {
        if (health < 1 && dead == false)
        {
            mapIndicator.SetActive(false);
            dead = true;
            return;
        }
        rotateToPlayer.RotateToPlayer();
    }

}
