using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GroundTrooper[] groundUnits;
    [HideInInspector]
    public List<GroundTrooper> GroundTroops = new List<GroundTrooper>();
    [HideInInspector]
    public List<GameObject> airFighters = new List<GameObject>();
    public GameObject particalEffect, fighterPrefab;
    public float spawnZOffset;
    public int round;

    public GameController gc;
    [HideInInspector]
    public PlayerManager pm;
    [HideInInspector]
    public ScoreManager cm;

    private Vector3 spawnDimensions;
    // Use this for initialization
    void Start()
    {
        spawnDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        pm = FindObjectOfType<PlayerManager>();
        gc = FindObjectOfType<GameController>();
        cm = FindObjectOfType<ScoreManager>();
        Debug.Log(spawnDimensions);


        
    }
	
	// Update is called once per frame
	void Update () {
        RoundController();
        
    }

    public void RoundController()
    {


        if (GroundTroops.Count == 0)
        {
            SpawnNewRound((7 - airFighters.Count), 10 +(round * 2));
        }
        if(airFighters.Count == 0)
        {
            cm.GameOver();
        }

    }
    public void SpawnNewFighter()
    {
       GameObject figher = Instantiate(fighterPrefab, GenerateMovePoint(), Quaternion.identity);
        airFighters.Add(figher);


    }
    public void SpawnGroundTrooper()
    {
        GroundTrooper gt = new GroundTrooper();
        int r = Random.Range(0, 101);
        int index = 0;
        if (r >= 80)
        {
            index= 1;
        }
        if (r >= 90)
        {
            index = 2;
        }

        //make  trooper (need to clean this!!)
        gt.name = groundUnits[index].name;
        gt.healthpoints = groundUnits[index].healthpoints;
        gt.scorepoint = groundUnits[index].scorepoint;
        gt.damage = groundUnits[index].damage;
        gt.prefab = Instantiate(groundUnits[index].prefab, new Vector3(-15, -1.7f, 0), Quaternion.identity);
        GroundTroops.Add(gt);
    }
    public void KillGroundTroops(Vector3 pos, float blastRange, float bonuspoints)
    {
        int count = 0;
        for (int i = 0; i < GroundTroops.Count; i++)
        {

            if ((pos.x - blastRange) < GroundTroops[i].GetPosTrooper().x && (pos.x + blastRange) > GroundTroops[i].GetPosTrooper().x && !GroundTroops[i].isDeath)
            {
                if ((pos.y - blastRange) < GroundTroops[i].GetPosTrooper().y && (pos.y + blastRange) > GroundTroops[i].GetPosTrooper().y)
                {
                    //destroy trooper  
                    cm.AddScore(GroundTroops[i].scorepoint + bonuspoints);

                    //---! list.remove() doesn't work (quick fix) !---
                    GroundTroops[i].isDeath = true;
                    GroundTroops[i].prefab.active = false;

                    //spawsblood
                    GameObject partical = Instantiate(particalEffect, pos, Quaternion.identity);
                    Destroy(partical, 3f);
                    count++;

                   
                }
            }
        }
        if (CheckIfTrooperIsDeath())
        {
            GroundTroops.Clear();
        }

    }
    private bool CheckIfTrooperIsDeath()
    {
        bool result = false;
        int count = 0;
        for (int i = 0; i < GroundTroops.Count; i++)
        {
            if (GroundTroops[i].isDeath)
            {
                count++;
            }
        }
        if(count == GroundTroops.Count)
        {
            result = true;
        }
        return result;
    }

    public void SpawnNewRound(int amountfighter,int amountTroopers)
    {
        round++;
        cm.UpdateRound(round);
        for (int i = 0; i < amountfighter; i++)
        {
            SpawnNewFighter();
        }
        for (int i = 0; i < amountTroopers; i++)
        {
            SpawnGroundTrooper();
        }


    }
    public Vector3 GenerateMovePoint()
    {
        float x = Random.Range(-spawnDimensions.x, spawnDimensions.x);
        float y = Random.Range(-(spawnDimensions.y - spawnZOffset), spawnDimensions.y);
        return new Vector3(x, y, 10f);
    }
    public GroundTrooper ReturnTrooper(GameObject prefab)
    {

        for (int i = 0; i < GroundTroops.Count; i++)
        {
            if(GroundTroops[i].prefab == prefab)
            {
                return GroundTroops[i];
            }

        }
        return null;
    }

    [System.Serializable]
    public class GroundTrooper
    {
        public string name;
        public GameObject prefab;
        public float scorepoint;
        public float healthpoints;
        public bool isDeath = false;
        public float damage;

        public Vector3 GetPosTrooper()
        {
            return prefab.transform.position;
        }
        
    }
}
