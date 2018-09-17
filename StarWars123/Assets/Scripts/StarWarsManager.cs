using System.Collections.Generic;
using UnityEngine;

public class StarWarsManager : MonoBehaviour {

    public int MaxObjectAmount;

    public GameObject[] SpawnableObjects;

    public List<StarWarsObject> ObjectList;

    protected GameObject CurrentSpawnedObject;

    protected bool IsSpawnable {
        get {
            return ObjectList.Count < MaxObjectAmount;
        }
    }

    public virtual void InitializeObject(int index) {
        CurrentSpawnedObject = Instantiate(SpawnableObjects[index], Vector3.zero,
            SpawnableObjects[index].transform.rotation);

        ObjectList.Add(CurrentSpawnedObject.GetComponent<StarWarsObject>());
    }

    public virtual void InitializeObject(int index, Vector3 position) {

        CurrentSpawnedObject = Instantiate(SpawnableObjects[index], Vector3.zero,
            SpawnableObjects[index].transform.rotation);

        ObjectList.Add(CurrentSpawnedObject.GetComponent<StarWarsObject>());
    }
}