using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObjectManager : StarWarsManager {

    public Text WaveTimerText;

    private int WaveCount;
    private int WaveDelay;

    private void Start() {
        WaveCount = 1;
        WaveDelay = 10;

        StartCoroutine(EnemyWaveSpawner());
        StartCoroutine(EnemyMovement());
    }

    public override void InitializeObject(int index, Vector3 position) {

        if (IsSpawnable) {
            base.InitializeObject(index);

            CurrentSpawnedObject.transform.position = position;
        }
    }

    private IEnumerator EnemyWaveSpawner() {

        while (true) {

            int waveTimer = WaveDelay;

            while (waveTimer > 0) {
                WaveTimerText.text = "Wave: " + WaveCount.ToString() + " in: " + waveTimer.ToString();
                waveTimer--;
                yield return new WaitForSeconds(1);
            }

            List<Vector3> tempSpawnPosistions = GetEnemySpawnLocation();

            for (int i = 0; i < MaxObjectAmount; i++) {

                int randomIndex = Random.Range(0, tempSpawnPosistions.Count);

                InitializeObject(0, tempSpawnPosistions[randomIndex]);
                tempSpawnPosistions.RemoveAt(randomIndex);
            }

            Debug.Log("Spawn enemy wave..");

            WaveCount++;
            MaxObjectAmount++;
            WaveDelay++;

            waveTimer = WaveDelay;

            yield return null;
        }
    }

    private IEnumerator EnemyMovement() {

        while (true) {
            foreach (EnemyObject enemy in ObjectList) {
                GameObject e = enemy.gameObject;
                e.gameObject.transform.Translate(-enemy.MovementSpeed, 0, 0);
            }

            yield return null;
        }
    }

    private static List<Vector3> GetEnemySpawnLocation() {

        List<Vector3> spawnLocations = new List<Vector3>();

        Vec2[] verticePositions = TerrainGen.TerrainInstance.GetVerticePositions();
        Vec2[] nodePositions = Grid.GridInstance.GetNodePositions();

        for (int i = 0; i < 11; i++) {
            spawnLocations.Add(new Vector3(nodePositions[i].X, verticePositions[i].Zf, nodePositions[i].Z));
        }

        return spawnLocations;
    }
}
