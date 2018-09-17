using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class TerrainGen : MonoBehaviour {

    private Mesh _terrainMesh;

    private Vector3[] _vertices;

    public GameObject DebugVertice;

    public static TerrainGen TerrainInstance;

    private void Awake() {

        TerrainInstance = this;

        _terrainMesh = GetComponent<MeshFilter>().mesh;

        _vertices = _terrainMesh.vertices;

        for (int i = 18; i < _vertices.Length; i += 11) {
            _vertices[i] = new Vector3(_vertices[i].x, _vertices[i].y + Random.Range(0.5f, 1.5f), _vertices[i].z);
        }

        for (int i = 0; i <= 5; i++) {

            for (int j = i, k = 0; k < 11; j += 11) {

                _vertices[j] = new Vector3(_vertices[j].x, _vertices[j].y - Random.Range(0.1f, 0.7f), _vertices[j].z);
                k++;
            }
        }

        _terrainMesh.vertices = _vertices;

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = _terrainMesh;
    }

    public Vec2[] GetVerticePositions() {

        List<Vec2> spawnLocations = new List<Vec2>();

        for (int i = 0, j = 0; j < 11; i += 11, j++)
            spawnLocations.Add(new Vec2(_vertices[i].x, _vertices[i].y));

        return spawnLocations.ToArray();
    }

    public IEnumerator DEBUG_InstantiatePrefab() {
        Debug.Log(_terrainMesh.vertices.Length);
        for (int i = 0; i < _terrainMesh.vertices.Length; i++) {
            Instantiate(DebugVertice, new Vector3(_terrainMesh.vertices[i].x, 0, _terrainMesh.vertices[i].z), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
