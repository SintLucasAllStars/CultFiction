using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable All

public class Grid : MonoBehaviour {
    public Node[,] GridNodes;
    public Lane[] GridLanes;

    public GameObject DebugPrefab1, DebugPrefab2;

    public static Grid GridInstance;

    public int WIDTH, HEIGHT;
    public int OFFSET;

    public enum GridStates {
        PlacingObjectMode,
        IdleMode
    };

    public GridStates State;

    private void Awake() {
        GridInstance = this;
        InitializeGrid(new Vec2(0, 0));
    }

    private void InitializeGrid(Vec2 position) {
        GridNodes = new Node[WIDTH, HEIGHT];
        GridLanes = new Lane[HEIGHT];

        for (int z = 0; z < HEIGHT; z++) {
            GridLanes[z] = new Lane();

            for (int x = 0; x < WIDTH; x++) {
                GridNodes[x, z] = new Node(new Vec2(x * OFFSET, z * OFFSET) + position);

                Node node = GridNodes[x, z];

                SetNodeAvailability(x, node);

                DEBUG_InstantiateNode(node);
            }
        }

        State = GridStates.IdleMode;
    }

    private static void SetNodeAvailability(int x, Node node) {
        if (x < 2)
            node.PlayerNodeAvailable = true;
        else
            node.EnemyNodeAvailable = true;
    }

    public IEnumerator PlaceObjects(GameObject obj) {

        State = GridStates.PlacingObjectMode;

        while (State == GridStates.PlacingObjectMode) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit)) {
                Vec2 newPosition = new Vec2((int)hit.point.x, (int)hit.point.z);

                Node node = GetPlayerAvailableNode(newPosition.X, newPosition.Z);

                if (node != null)
                    obj.transform.position = new Vector3(node.Position.X, hit.point.y + 0.5f, node.Position.Z);
                else {
                    Debug.Log("Node Unavailable..");
                }

                if (Input.GetKeyDown(KeyCode.B)) {

                    State = GridStates.IdleMode;
                    SetPlayerTileUnavailable(node);

                    obj.GetComponent<StarWarsObject>().SetLane();
                    obj.GetComponent<StarWarsObject>().InitializeObject();
                }

                if (Input.GetKeyDown(KeyCode.Mouse1)) {
                    State = GridStates.IdleMode;
                    Destroy(obj);
                }
            }

            yield return null;
        }
    }

    public void SetPlayerTileUnavailable(Node node) {
        node.PlayerNodeAvailable = false;
    }

    public Node GetPlayerAvailableNode(int x, int z) {
        int finalX = x / OFFSET;
        int finalZ = z / OFFSET;

        if (finalX < 2 && finalX >= 0 && finalZ >= 1 && finalZ < HEIGHT)
            return GridNodes[finalX, finalZ];

        return null;
    }

    public Vec2[] GetNodePositions() {
        List<Vec2> spawnLocations = new List<Vec2>();

        for (int i = 0; i < WIDTH; i++) {
            spawnLocations.Add(new Vec2(GridNodes[WIDTH - 1, i].Position.X, GridNodes[WIDTH - 1, i].Position.Z));
        }

        return spawnLocations.ToArray();
    }

    private void DEBUG_InstantiateNode(Node node) {
        Instantiate(node.PlayerNodeAvailable ? DebugPrefab1 : DebugPrefab2,
            new Vector3(node.Position.X, 0, node.Position.Z), Quaternion.identity);
    }
}

public struct Vec2 {

    public int X;
    public int Z;

    public float Xf;
    public float Zf;

    public Vec2(int x, int z) {
        this.Xf = 0;
        this.Zf = 0;

        this.X = x;
        this.Z = z;
    }

    public Vec2(float x, float z) {
        this.X = 0;
        this.Z = 0;

        this.Xf = x;
        this.Zf = z;
    }

    public static Vec2 operator +(Vec2 a, Vec2 b) {
        Vec2 vec2 = new Vec2();
        vec2.X = a.X + b.X;
        vec2.Z = a.Z + b.Z;
        return vec2;
    }
}

public class Node {

    public Vec2 Position;
    public bool EnemyNodeAvailable, PlayerNodeAvailable;

    public Node(Vec2 position) {
        this.Position = position;
    }
}

public class Lane {

    public List<StarWarsObject> currentTurretsInLane;
    public List<StarWarsObject> currentEnemiesInLane;

    public Lane() {
        currentTurretsInLane = new List<StarWarsObject>();
        currentEnemiesInLane = new List<StarWarsObject>();
    }

    public void AddTurretsInLane(StarWarsObject turret) {
        currentTurretsInLane.Add(turret);
    }

    public void AddEnemiesInLane(StarWarsObject enemy) {
        currentEnemiesInLane.Add(enemy);
    }
}