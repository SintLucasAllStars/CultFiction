using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject prefab;

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

    public Tile[,] tiles;

    List<Rect> rooms = new List<Rect>();

    int[] regions;

    int currentRegion = -1;

    [SerializeField]
    private Dungeon dungeon;

    private void Start()
    {
        tiles = new Tile[dungeon.width + 1, dungeon.height + 1];

        StartCoroutine(AddRooms());
        //StartCoroutine(CreateMaze());
    }

    private IEnumerator AddRooms()
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

            Debug.Log("Times");

            rooms.Add(room);

            StartRegion();
            for (int i = x; i < (x + width); i++)
            {
                for (int j = y; j < (y + height); j++)
                {
                    Vector2 pos = new Vector2(i, j);
                    GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                    tiles[i, j].SetTile(go);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        StartCoroutine(CreateMaze());
    }

    private IEnumerator CreateMaze()
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
                GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                tiles[(int)pos.x, (int)pos.y].SetTile(go);
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

                        go = Instantiate(prefab, cell + dir, Quaternion.identity);
                        tiles[(int)(cell.x + dir.x), (int)(cell.y + dir.y)].SetTile(go);

                        go = Instantiate(prefab, cell + (dir * 2), Quaternion.identity);
                        tiles[(int)(cell.x + (dir.x * 2)), (int)(cell.y + (dir.y * 2))].SetTile(go);

                        cells.Add(cell + (dir * 2));
                        lastDir = dir;
                    }
                    else
                    {
                        cells.RemoveAt(cells.Count - 1);
                        lastDir = Vector2.zero;
                    }
                    //yield return new WaitForEndOfFrame();
                    yield return new WaitForSeconds(0.0000000001f);
                }
            }
        }
    }

    private void StartRegion()
    {
        currentRegion++;
    }
}
