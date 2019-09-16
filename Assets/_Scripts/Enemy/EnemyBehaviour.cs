using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Ship
{
    public GameObject lockOn;

    public LockOnLookat rotateToPlayer;
    
    void Update()
    {
        rotateToPlayer.RotateToPlayer();
    }

}
