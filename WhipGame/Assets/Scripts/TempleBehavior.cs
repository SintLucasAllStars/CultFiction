using System.Collections.Generic;
using UnityEngine;

public class TempleBehavior : MonoBehaviour
{
    public List<GameObject> ducksCollected;
    [SerializeField] public int duckCount = 0;
    public GameObject DuckPrefab;


    void Update()
    {
        for (int i = 0; i < ducksCollected.Count; i++)
        {
            if (i < duckCount)
            {
                ducksCollected[i].gameObject.SetActive(true);
            }

            else
            {
                ducksCollected[i].gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AddDuck();
        }

        if(duckCount == 4)
        {
            Debug.Log("spawn temple");
        }
    }

    public void AddDuck()
    {
        if (duckCount < 4)
        {
            duckCount++;
        }
    }
}
