using System.Collections;
using UnityEngine;

public abstract class StarWarsObject : MonoBehaviour {

    public Lane Lane;

    protected bool IsAlive;

    public GameObject Explosions;
    public GameObject[] Bullets;
    public Transform[] BulletSpawnTransforms, ExplosionsSpawnTransforms;
    public Rigidbody[] RigidBodyObjects;

    public float Health, MinDistance, ReloadTime, FirePower;

    public void InitializeObject() {

        IsAlive = true;
        StartCoroutine(Shoot());
    }

    protected abstract StarWarsObject GetTarget();
    protected abstract Vector3 GetShootDirection();
    protected abstract bool IsFirstInLane(StarWarsObject sObj);
    public abstract void CheckDestruction(StarWarsObject obj);

    public virtual void SetLane() {
        Lane = Grid.GridInstance.GridLanes[(int)gameObject.transform.position.z / Grid.GridInstance.OFFSET];
    }

    private IEnumerator Shoot() {
        while (IsAlive) {
            if (GetTarget() != null && IsFirstInLane(this)) {
                yield return new WaitForSeconds(ReloadTime);

                Vector2 gameObjectPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
                Vector2 targetPos = new Vector2(GetTarget().transform.position.x, GetTarget().transform.position.z);

                float distance = GetDistance(targetPos, gameObjectPos);

                if (distance < MinDistance) {
                    for (int i = 0; i < BulletSpawnTransforms.Length; i++) {
                        Bullets[i].SetActive(true);
                        Bullets[i].transform.position = BulletSpawnTransforms[i].position;
                        RigidBodyObjects[i].AddForce(GetShootDirection() * FirePower);
                    }

                    yield return new WaitForSeconds(0.05f);

                    for (int i = 0; i < BulletSpawnTransforms.Length; i++)
                        Bullets[i].SetActive(false);

                    Instantiate(Explosions,
                        GetTarget().ExplosionsSpawnTransforms[
                            Random.Range(0, GetTarget().ExplosionsSpawnTransforms.Length)].position,
                        Quaternion.identity);

                    DecreaseTargetHealth();
                }
            }
            yield return null;
        }
    }

    private void DecreaseTargetHealth() {
        StarWarsObject obj = GetTarget();
        obj.Health -= FirePower;
        obj.CheckDestruction(obj);
    }

    private static float GetDistance(Vector2 target, Vector2 position) {
        float x = Mathf.Abs(target.x - position.x);
        float z = Mathf.Abs(target.y - position.y);

        return Mathf.Sqrt((x * x) + (z * z));
    }
}