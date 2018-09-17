using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] EnemyTipes;
    public List<Enemy> enemys = new List<Enemy>();
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
    void Update()
    {
        RoundController();

    }
    public void RoundController()
    {

        if (enemys.Count == 0)
        {
            SpawnNewRound((7 - airFighters.Count), 10 + (round * 2));
        }
        if (airFighters.Count == 0)
        {
            cm.GameOver();
        }

    }
    public void SpawnNewFighter()
    {
        GameObject figher = Instantiate(fighterPrefab, GenerateMovePoint(), Quaternion.identity);
        airFighters.Add(figher);


    }
    public void SpawnGroundUnit(Vector3 spawnPos, int Newindex)
    {

        int r = Random.Range(0, 101);
        int index = 0;
        for (int i = 0; i < EnemyTipes.Length; i++)
        {
            if(r >= (100 - EnemyTipes[i].GetComponent<Enemy>().spawnPrecentage)){
                index = i;
            }
        }
        if (Newindex > -1)
        {
            index = Newindex;
        }
      GameObject g = Instantiate(EnemyTipes[index], spawnPos, Quaternion.identity);
      enemys.Add(g.GetComponent<Enemy>());
    }
    public void KillGroundTroops(Vector3 pos, float blastRange, float bonuspoints)
    {

        for (int i = 0; i < enemys.Count; i++)
        {
            if ((pos.x - blastRange) < enemys[i].transform.position.x && (pos.x + blastRange) > enemys[i].transform.position.x && !enemys[i].isDeath)
            {
                if ((pos.y - blastRange) < enemys[i].transform.position.y && (pos.y + blastRange) > enemys[i].transform.position.y)
                {
                    enemys[i].IsDeath();
                    cm.AddScore(enemys[i].killPoints);
                    //spawn BloodPartical
                    GameObject partical = Instantiate(particalEffect, pos, Quaternion.identity);
                    Destroy(partical, 3f);

                }
            }

        }
        if (CheckIfEnemysAreDeath())
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].DestroyPrefab(2);
            }
            enemys.Clear();
        }
    }
    private bool CheckIfEnemysAreDeath()
    {
        int count = 0;
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].isDeath)
            {
                count++;
            }
        }
        if(count == enemys.Count)
        {
            Debug.Log(count);
            return true;
        }
        return false;
    }
    public void SpawnNewRound(int amountfighter, int amountTroopers)
    {
        round++;
        cm.UpdateRound(round);
        for (int i = 0; i < amountfighter; i++)
        {
            SpawnNewFighter();
        }
        for (int i = 0; i < amountTroopers; i++)
        {
            SpawnGroundUnit(new Vector3(-15, -1.7f, 0) ,- 1);
        }


    }
    public Vector3 GenerateMovePoint()
    {
        float x = Random.Range(-spawnDimensions.x, spawnDimensions.x);
        float y = Random.Range(-(spawnDimensions.y - spawnZOffset), spawnDimensions.y);
        return new Vector3(x, y, 10f);
    }

}