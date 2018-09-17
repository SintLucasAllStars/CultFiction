using UnityEngine;

public class TurretObject : StarWarsObject {


    protected override StarWarsObject GetTarget() {
        return Lane.currentEnemiesInLane.Count <= 0 ? null : Lane.currentEnemiesInLane[0];
    }

    protected override Vector3 GetShootDirection() {
        return -Vector3.left;
    }

    protected override bool IsFirstInLane(StarWarsObject sObj) {
        return true;
    }

    public override void SetLane() {
        base.SetLane();
        Lane.AddTurretsInLane(this);
    }
}