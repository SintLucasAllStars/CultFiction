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
    }

    List<Rect> rooms = new List<Rect>();

    int[] regions;

    int currentRegion = -1;

    [SerializeField]
    private Dungeon dungeon;

    private void Start()
    {
        StartCoroutine(AddRooms());
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
                    Instantiate(prefab, pos, Quaternion.identity);
                    yield return new WaitForSeconds(.00001f);
                }
            }
        }
    }

    private void StartRegion()
    {
        currentRegion++;
    }
}
