using System.Collections;
using UnityEngine;

public class EnemyObject : StarWarsObject {

    public float MovementSpeed;

    private void Start() {
        SetLane();
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
        return Lane.currentEnemiesInLane.IndexOf(sObj) == 0;
    }

    public override void SetLane() {
        base.SetLane();
        Lane.AddEnemiesInLane(this);
    }

    public override void CheckDestruction(StarWarsObject obj) {
        if (Health <= 0) {
            IsAlive = false;
            StarWarsManager.StarWarsManagerInstance.RemoveObject(this);
            Lane.currentEnemiesInLane.Remove(this);
            StartCoroutine(EnemyDestructionAnimation());
            Destroy(gameObject, 1.5f);

            //Upgrade Turret (Not Balanced)
            obj.FirePower += 5;
            obj.Health += 5;
        }
    }

    private IEnumerator EnemyDestructionAnimation() {
        float rSpeed = 0.5f;
        float tSpeed = 0.01f;
        while (gameObject.activeInHierarchy) {
            transform.Rotate(new Vector3(rSpeed, 0, 0));
            transform.Translate(new Vector3(0, -tSpeed, 0));
            Debug.Log("kek");
            yield return null;
        }

    }

    public bool DestinationReached {
        get {
            return (int)transform.position.x <= 5;
        }
    }
}