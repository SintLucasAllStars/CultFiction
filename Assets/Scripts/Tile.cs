using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Tile
{
    public GameObject tile;
    public Vector2 pos;

    public bool HasTile()
    {
        if(tile != null)
        {
            return true;
        }

        return false;
    }

    public void SetTile(GameObject go)
    {
        tile = go;
        pos = go.transform.position;
    }
}
