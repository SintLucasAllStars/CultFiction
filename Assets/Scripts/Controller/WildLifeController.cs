using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildLifeController : Singleton<WildLifeController>
{
	public GameObject[] smallAnimals;
	public GameObject[] bigAnimals;
	public List<CharacterController> smallSpawnedAnimals = new List<CharacterController>();
	[SerializeField]
	List<CharacterController> bigSpawnedAnimals = new List<CharacterController>();

	public List<Transform> wayPoints;

	[SerializeField]
	int maxSmallAnimals;
	[SerializeField]
	int maxBigAnimals;
	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < maxSmallAnimals;i++){
			SpawnAnimal(true, 0);
		}
		for (int i = 0; i < maxSmallAnimals; i++)
        {
            SpawnAnimal(false, 0);
        }
	}

	// Update is called once per frame
	void Update()
	{
		if(smallSpawnedAnimals.Count < maxSmallAnimals){
			SpawnAnimal(true, 0);
		}
	}
	void SpawnAnimal(bool small, int indexOffAnimal = 1000)
	{
		Vector3 spawnPos = wayPoints[Random.Range(0, wayPoints.Count)].position;
		int index = indexOffAnimal;

		if (small)
		{
			if (smallSpawnedAnimals.Count < maxSmallAnimals)
			{
				if (index == 10000)
				{
					index = Random.Range(0, smallAnimals.Length);
				}
				GameObject g = Instantiate(smallAnimals[index], spawnPos, Quaternion.identity,transform);
				smallSpawnedAnimals.Add(g.GetComponent<CharacterController>());
				g.GetComponent<WayPointWalkAbility>().wayPoints = wayPoints;
			}
		}
		else
		{
			if (bigSpawnedAnimals.Count < maxBigAnimals)
			{
				if (index == 10000)
				{
					index = Random.Range(0, bigAnimals.Length);
				}
				GameObject g = Instantiate(bigAnimals[index], spawnPos, Quaternion.identity,transform);
				bigSpawnedAnimals.Add(g.GetComponent<CharacterController>());
                g.GetComponent<WayPointWalkAbility>().wayPoints = wayPoints;
			}
		}
	}
}
