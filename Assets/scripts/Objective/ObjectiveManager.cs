using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    private List<GameObject> barrelStorages = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterBarrelStorage(GameObject barrelStorage)
    {
        if (barrelStorage.name == "BarrelStorage")
        {
            barrelStorages.Add(barrelStorage);
            print("Item added");
        }
    }

    public void RevmoveBarrelStorage(GameObject barrelStorage)
    {
        barrelStorages.Remove(barrelStorage);
        print("Item removed");
        CheckForObjectiveCompletion();
    }

    private void CheckForObjectiveCompletion()
    {
        if (barrelStorages.Count <= 0)
        {
            //completed objective
            print("Objective completed");
        }
    }
}
