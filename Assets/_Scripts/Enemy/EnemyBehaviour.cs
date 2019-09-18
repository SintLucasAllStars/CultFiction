using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Ship
{
    public enum TypeOfEnemy { generator, turret}
    public TypeOfEnemy enemyType;

    public GameObject lockOn;
    public GameObject mapIndicator;
    public bool dead;

    public LockOnLookat rotateToPlayer;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (health < 1 && dead == false)
        {
            mapIndicator.SetActive(false);
            dead = true;
            UpdateManager();
            return;
        }
        rotateToPlayer.RotateToPlayer();
    }

    private void UpdateManager()
    {
        switch (enemyType)
        {
            case TypeOfEnemy.generator:
                gm.ShieldDestroyed();
                break;
            case TypeOfEnemy.turret:
                gm.TurretDestroyed();
                break;
            default:
                break;
        }
    }

}
