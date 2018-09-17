using UnityEngine;

public class EnemyObject : StarWarsObject {

    public float MovementSpeed;

    private void Start() {
        InitializeObject();
        MovementSpeed = 0.002f;
    }

    protected override StarWarsObject GetTarget() {
        return Lane.currentTurretsInLane.Count <= 0 ? null : Lane.currentTurretsInLane[0];
    }

    protected override Vector3 GetShootDirection() {
        return Vector3.left;
    }

    protected override bool IsFirstInLane(StarWarsObject sObj) {
        return Lane.currentEnemiesInLane.IndexOf(sObj) == 0 ? true : false;
    }

    public override void SetLane() {
        base.SetLane();
        Lane.AddEnemiesInLane(this);
    }
}