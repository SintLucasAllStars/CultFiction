using System.Collections.Generic;
using UnityEngine;

public abstract class StarWarsManager : MonoBehaviour {

    public int MaxObjectAmount;

    public GameObject[] SpawnableObjects;

    public List<StarWarsObject> ObjectList;

    protected GameObject CurrentSpawnedObject;

    public static StarWarsManager StarWarsManagerInstance;

    protected abstract void SetStarWarsManagerInstance();

    protected bool IsSpawnable {
        get {
            return ObjectList.Count < MaxObjectAmount;
        }
    }

    protected virtual void InitializeObject(int index) {
        CurrentSpawnedObject = Instantiate(SpawnableObjects[index], Vector3.zero,
            SpawnableObjects[index].transform.rotation);

        ObjectList.Add(CurrentSpawnedObject.GetComponent<StarWarsObject>());
    }

    public virtual void InitializeObject(int index, Vector3 position, int upgrade) {

        CurrentSpawnedObject = Instantiate(SpawnableObjects[index], Vector3.zero,
            SpawnableObjects[index].transform.rotation);

        StarWarsObject sObj = CurrentSpawnedObject.GetComponent<StarWarsObject>();

        sObj.Health = Mathf.Pow(upgrade, 3);
        sObj.FirePower = Mathf.Pow(upgrade, 2);

        ObjectList.Add(CurrentSpawnedObject.GetComponent<StarWarsObject>());
    }

    public void RemoveObject(StarWarsObject starWarsObject) {
        ObjectList.Remove(starWarsObject);
    }
}