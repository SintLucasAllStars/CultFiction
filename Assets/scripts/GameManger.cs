using System;
using UnityEngine;

public class GameManger : MonoBehaviour {
    public int Score;

    public void ScoreIncrement() {
        Score++;
    }

    public void DeathState() {
        print("You're dead");
    }
}