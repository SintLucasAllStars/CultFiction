using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public Transform left, right, up, down, front, back;

    [Space(5)]
    public GameObject[] foodOnRoadPrefab;
    public GameObject[] foodInCarPrefab;

    public int timeBetweenSpawn;
    Vector3 offset = new Vector3(0, 3, 0);

    [Space(5)]
    public float explosionForce = 300f;
    public float explosionRadius = 10f;

    Driving m_driving;
    ObstacleManager m_obstacleManger;

    private void Start()
    {
        m_driving = FindObjectOfType<Driving>();
        m_obstacleManger = FindObjectOfType<ObstacleManager>();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            StartCoroutine(FoodSpawner());
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    SpawnFood(0);
        //    ExplodeForceOnFood();
        //}
            
    }

    #region InCar
    // maybe in the future as argument the object self it can spawn different kind of food
    public void SpawnFood(int index)
    {
        GameObject spawnedFood = Instantiate(foodInCarPrefab[index], GetRandomBorders(), Quaternion.identity, m_driving.transform);
        m_driving.AddRigidbody(spawnedFood.GetComponent<Rigidbody>());
    }

    public void ExplodeForceOnFood()
    {
        Vector3 randomExplosionPoint = GetRandomBorders();
        Rigidbody[] foodRbs = FindObjectOfType<Driving>().distractionRb.ToArray();
        for (int i = 0; i < foodRbs.Length; i++)
        {
            foodRbs[i].AddExplosionForce(explosionForce, randomExplosionPoint, explosionRadius);
        }

    }
    #endregion

    #region OnRoad
    public Vector3 GetRandomBorders()
    {
        float offset = 1.5f;

        // the offset incase it will bugg out and leave the border
        Vector3 randomPos = new Vector3(
            Random.Range(left.position.x + offset, right.position.x - offset),
            Random.Range(down.position.y + offset, up.position.y - offset),
            Random.Range(back.position.z + offset, front.position.z - offset));
        return randomPos;
    }

    IEnumerator FoodSpawner()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        int prefabIndex = Random.Range(0, foodOnRoadPrefab.Length);
        Instantiate(foodOnRoadPrefab[prefabIndex], m_obstacleManger.GetRandomLaneSpawn() + offset, Quaternion.identity);
        StartCoroutine(FoodSpawner());
    }
    #endregion
}
