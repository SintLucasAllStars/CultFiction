using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] Path, Room, Wall;
    public GameObject region;
    private GameObject parent;
    public Camera cam;

    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public GameObject gate;

    [System.Serializable]
    public class Dungeon
    {
        public int width;
        public int height;

        public int numRoomTries;
        public int roomExtraSize = 0;

        [Range(0, 1)]
        public float mazeWindingPercent = 0.4f;
    }

    public static Tile[,] tiles;

    List<Rect> rooms = new List<Rect>();

    int[] regions;

    int currentRegion = -1;

    [SerializeField]
    private Dungeon dungeon;

    private void Start()
    {
        tiles = new Tile[dungeon.width + 1, dungeon.height + 1];
        AddRooms();
    }

    private void AddRooms()
    {
        for (int Iterations = 0; Iterations < dungeon.numRoomTries; Iterations++)
        {
            int size = Random.Range(1, 3 + dungeon.roomExtraSize) * 2 + 1;
            int rectangularity = Random.Range(0, 1 + size / 2) * 2;
            int width = size;
            int height = size;
            if(Random.value > .5f)
            {
                width += rectangularity;
            }
            else
            {
                height += rectangularity;
            }

            int x = Random.Range(0, (dungeon.width - width) / 2) * 2 + 1;
            int y = Random.Range(0, (dungeon.height - height) / 2) * 2 + 1;

            Rect room = new Rect(x, y, width, height);

            bool overlap = false;
            foreach(Rect other in rooms)
            {
                if(room.Overlaps(other))
                {
                    overlap = true;
                    break;
                }
            }

            if (overlap)
                continue;

            rooms.Add(room);

            StartRegion();
            for (int i = x; i < (x + width); i++)
            {
                for (int j = y; j < (y + height); j++)
                {
                    Vector2 pos = new Vector2(i, j);
                    int index = Random.Range(0, Room.Length - 1);
                    GameObject go = Instantiate(Room[index], pos, Quaternion.identity, parent.transform);
                    tiles[i, j].SetTile(go, TileType.Room);
                    tiles[i, j].Region = currentRegion;
                }
            }
        }
        CreateMaze();
    }

    private void CreateMaze()
    {
        for (int y = 1; y < dungeon.height; y += 2)
        {
            for (int x = 1; x < dungeon.width; x += 2)
            {
                if (tiles[x, y].HasTile()) //if position already has a tile
                    continue;

                Vector2 pos = new Vector2(x, y);

                List<Vector2> cells = new List<Vector2>();
                Vector2 lastDir = Vector2.zero;

                StartRegion();
                int index = Random.Range(0, Path.Length - 1);
                GameObject go = Instantiate(Path[index], pos, Quaternion.identity, parent.transform);
                tiles[(int)pos.x, (int)pos.y].SetTile(go, TileType.Path);
                tiles[(int)pos.x, (int)pos.y].Region = currentRegion;
                cells.Add(pos);

                while(cells.Count != 0)
                {
                    Vector2 cell = cells[cells.Count - 1];

                    List<Vector2> unmadeCells = new List<Vector2>();

                    List<Vector2Int> neighbours = new List<Vector2Int>();
                    neighbours.Add(new Vector2Int((int)cell.x + 1, (int)cell.y));
                    neighbours.Add(new Vector2Int((int)cell.x, (int)cell.y - 1));
                    neighbours.Add(new Vector2Int((int)cell.x, (int)cell.y + 1));
                    neighbours.Add(new Vector2Int((int)cell.x - 1, (int)cell.y));

                    foreach(Vector2Int neighbour in neighbours)
                    {
                        if (neighbour.x >= 1 && neighbour.x < dungeon.width - 1 &&
                            neighbour.y >= 1 && neighbour.y < dungeon.height - 1 &&
                            !tiles[neighbour.x, neighbour.y].HasTile())
                        {
                            Vector2 dir = neighbour - cell;
                            if (!tiles[(neighbour.x + (int)dir.x), (neighbour.y + (int)dir.y)].HasTile())
                                unmadeCells.Add(dir);
                        }
                    }

                    if (unmadeCells.Count != 0)
                    {
                        Vector2 dir;

                        if (unmadeCells.Contains(lastDir) && Random.value >= dungeon.mazeWindingPercent)
                        {
                            dir = lastDir;
                        }
                        else
                        {
                            dir = unmadeCells[Random.Range(0, unmadeCells.Count)];
                        }

                        index = Random.Range(0, Path.Length - 1);
                        go = Instantiate(Path[index], cell + dir, Quaternion.identity, parent.transform);
                        tiles[(int)(cell.x + dir.x), (int)(cell.y + dir.y)].SetTile(go, TileType.Path);
                        tiles[(int)(cell.x + dir.x), (int)(cell.y + dir.y)].Region = currentRegion;

                        index = Random.Range(0, Path.Length - 1);
                        go = Instantiate(Path[index], cell + (dir * 2), Quaternion.identity, parent.transform);
                        tiles[(int)(cell.x + (dir.x * 2)), (int)(cell.y + (dir.y * 2))].SetTile(go, TileType.Path);
                        tiles[(int)(cell.x + (dir.x * 2)), (int)(cell.y + (dir.y * 2))].Region = currentRegion;

                        cells.Add(cell + (dir * 2));
                        lastDir = dir;
                    }
                    else
                    {
                        cells.RemoveAt(cells.Count - 1);
                        lastDir = Vector2.zero;
                    }
                }
            }
        }
        ConnectRooms();
    }

    private void ConnectRooms()
    {
        StartRegion();
        List<Vector2> connectors = new List<Vector2>();
        GameObject go;

        foreach (Rect room in rooms)
        {
            Rect region = ConnectorRegions(room, 1f);

            for(int x = (int)region.x; x < (region.x + region.width + 1); x++)
            {
                for (int y = (int)region.y; y < (region.y + region.height + 1); y++)
                {
                    if (x >= 1 && x < dungeon.width - 1 &&
                        y >= 1 && y < dungeon.height - 1 &&
                        !tiles[x, y].HasTile())
                    {
                        Vector2 pos = new Vector2(x, y);

                        List<int> regions = new List<int>();

                        List<Vector2Int> neighbours = new List<Vector2Int>();
                        neighbours.Add(new Vector2Int((int)pos.x + 1, (int)pos.y));
                        neighbours.Add(new Vector2Int((int)pos.x, (int)pos.y - 1));
                        neighbours.Add(new Vector2Int((int)pos.x, (int)pos.y + 1));
                        neighbours.Add(new Vector2Int((int)pos.x - 1, (int)pos.y));

                        foreach(Vector2Int neighbour in neighbours)
                        {
                            if (neighbour.x >= 1 && neighbour.x < dungeon.width - 1 &&
                                neighbour.y >= 1 && neighbour.y < dungeon.height - 1 &&
                                tiles[neighbour.x, neighbour.y].HasTile())
                            {
                                if(!regions.Contains(tiles[neighbour.x, neighbour.y].Region))
                                {
                                    regions.Add(tiles[neighbour.x, neighbour.y].Region);
                                }
                            }
                        }

                        if(regions.Count == 2)
                        {
                            connectors.Add(new Vector2(x, y));
                        }
                    }
                }
            }

            Vector2 connector = connectors[Random.Range(0, connectors.Count)];

            int index = Random.Range(0, Path.Length - 1);
            go = Instantiate(Path[index], connector, Quaternion.identity, parent.transform);
            tiles[(int)connector.x, (int)connector.y].SetTile(go, TileType.Door);
            connectors.Remove(connector);

            if (Random.value < 0.2f)
            {
                connector = connectors[Random.Range(0, connectors.Count)];
                index = Random.Range(0, Path.Length - 1);
                go = Instantiate(Path[index], connector, Quaternion.identity, parent.transform);
                tiles[(int)connector.x, (int)connector.y].SetTile(go, TileType.Door);
            }
            connectors.Clear();
        }
        RemoveDeadEnds();
    }

    private void RemoveDeadEnds()
    {
        bool isDone = false;

        while (!isDone)
        {
            isDone = true;
            for (int y = 1; y < dungeon.height - 1; y++)
            {
                for (int x = 1; x < dungeon.width - 1; x++)
                {
                    if (tiles[x, y].tileType != TileType.Path)
                        continue;

                    int exits = 0;

                    List<Vector2Int> neighbours = new List<Vector2Int>();
                    neighbours.Add(new Vector2Int(x + 1, y));
                    neighbours.Add(new Vector2Int(x, y - 1));
                    neighbours.Add(new Vector2Int(x, y + 1));
                    neighbours.Add(new Vector2Int(x - 1, y));

                    foreach(Vector2Int neighbour in neighbours)
                    {
                        if(tiles[neighbour.x, neighbour.y].HasTile() && tiles[neighbour.x, neighbour.y].tileType != TileType.Wall)
                        {
                            exits++;
                        }
                    }

                    if (exits != 1)
                        continue;

                    isDone = false;
                    Destroy(tiles[x, y].tile);
                    int index = Random.Range(0, Wall.Length - 1);
                    GameObject go = Instantiate(Wall[index], new Vector2(x, y), Quaternion.identity, parent.transform);
                    tiles[x, y].SetTile(go, TileType.Wall);
                }
            }
        }

        CamBackground();
        PlaceEntities();
    }

    private void PlaceEntities()
    {
        int roomIndex = Random.Range(0, rooms.Count - 1);
        Rect room = rooms[roomIndex];

        Instantiate(playerPrefab, new Vector2(room.x + 1, room.y + 1), Quaternion.identity);

        StartRegion();
        parent.name = "Enemies";
        for (int i = 0; i < rooms.Count; i++)
        {
            if(i == rooms.Count - 1)
            {
                Rect rect = rooms[i];
                Vector2 pos = new Vector2(rect.x + rect.width - 1, rect.y + rect.height - 1);
                Instantiate(gate, pos, Quaternion.identity);
                Destroy(tiles[(int)pos.x, (int)pos.y].tile);
            }

            if (i == roomIndex)
                continue;


            for (int x = (int)rooms[i].x; x < (rooms[i].x + rooms[i].width); x++)
            {
                for (int y = (int)rooms[i].y; y < (rooms[i].y + rooms[i].height); y++)
                {
                    if (Random.value < 0.08f)
                    {
                        Instantiate(enemyPrefab, new Vector2((int)x, (int)y), Quaternion.identity, parent.transform);
                    }
                }
            }
        }
    }

    private void CamBackground()
    {
        for (int y = 0; y < dungeon.height; y++)
        {
            for (int x = 0; x < dungeon.width; x++)
            {
                if(!tiles[x, y].HasTile())
                {
                    int index = Random.Range(0, Wall.Length - 1);
                    GameObject go = Instantiate(Wall[index], new Vector2(x, y), Quaternion.identity, parent.transform);
                    tiles[x, y].SetTile(go, TileType.Wall);
                }
            }
        }

        cam.backgroundColor = Color.black;
    }

    private void StartRegion()
    {
        currentRegion++;
        parent = Instantiate(region, Vector2.zero, Quaternion.identity);
    }

    public static bool PosisValid(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;

        return (tiles[x, y].tileType != TileType.Wall);
    }

    private Rect ConnectorRegions(Rect rect, float delta)
    {
        return new Rect(rect.x - delta, rect.y - delta, rect.width + delta, rect.height + delta);
    }
}
