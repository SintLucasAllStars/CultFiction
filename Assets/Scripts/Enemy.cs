using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public State state = State.Idle;
    private float speed;
    private float minAttackDistance, maxAttackDistance;
    private float extraReloadDistance;
    private Vector2 playerLostPos;

    private void Update()
    {
        
    }

    private void ChangeState(State newState)
    {
        state = newState;
    }
}

public enum State { Idle, Attack, Search, Dead }