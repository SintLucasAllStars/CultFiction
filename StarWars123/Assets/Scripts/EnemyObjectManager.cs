using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObjectManager : StarWarsManager {

    public Text WaveTimerText;

    private int _waveCount;
    private int _waveDelay;
    private int _upgrade;

    private bool _isGameOver;

    private List<Vector3> _tempSpawnPosistions;

    private void Start() {
        SetStarWarsManagerInstance();
    }

    public void InitializeEnemyManger() {

        MenuManager.MenuManagerInstance.EnableInGameUi();
        MenuManager.MenuManagerInstance.DisableStartGameUi();

        _upgrade = 1;
        _waveCount = 1;
        _waveDelay = 15;

        _isGameOver = false;

        StartCoroutine(EnemyWaveSpawner());
        StartCoroutine(EnemyMovement());
    }

    protected override void SetStarWarsManagerInstance() {
        StarWarsManagerInstance = this;
    }

    public override void InitializeObject(int index, Vector3 position, int upgrade) {

        if (IsSpawnable) {
            base.InitializeObject(index);

            CurrentSpawnedObject.transform.position = position;
        }
    }

    private IEnumerator EnemyWaveSpawner() {

        while (!_isGameOver) {

            int waveTimer = _waveDelay;

            _tempSpawnPosistions = GetEnemySpawnLocation();

            while (waveTimer > 0) {
                WaveTimerText.text = "Wave: " + _waveCount.ToString() + " in: " + waveTimer.ToString();
                waveTimer--;
                yield return new WaitForSeconds(1);
            }

            for (int i = 0; i < MaxObjectAmount; i++) {

                int randomIndex = Random.Range(0, _tempSpawnPosistions.Count);

                InitializeObject(0, _tempSpawnPosistions[randomIndex], _upgrade);
                _tempSpawnPosistions.RemoveAt(randomIndex);
            }

            Debug.Log("Spawn enemy wave..");

            _waveCount++;

            if (MaxObjectAmount + 3 <= 11)
                MaxObjectAmount += 3;

            if (_waveCount % 2 == 0)
                _upgrade++;

            waveTimer = _waveDelay;

            yield return null;
        }
    }

    private IEnumerator EnemyMovement() {

        while (!_isGameOver) {
            foreach (var o in ObjectList) {

                var enemy = (EnemyObject)o;

                if (!enemy.DestinationReached) {
                    GameObject e = enemy.gameObject;
                    e.gameObject.transform.Translate(-enemy.MovementSpeed, 0, 0);
                } else {
                    _isGameOver = true;
                    MenuManager.MenuManagerInstance.DisableIngameUi();
                    MenuManager.MenuManagerInstance.EnableGameOverUi();
                    Debug.Log("GameOver..");
                }
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
