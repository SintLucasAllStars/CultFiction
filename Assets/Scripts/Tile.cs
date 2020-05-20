using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Tile
{
    public GameObject tile;
    public Vector2 pos;
    public TileType tileType;

    private int region;
    public int Region
    {
        get { return region; }
        set
        {
            region = value;
        }
    }

    public bool HasTile()
    {
        if(tile != null)
        {
            return true;
        }

        return false;
    }

    public void SetTile(GameObject go, TileType type)
    {
        tile = go;
        pos = go.transform.position;
        tileType = type;
    }


}

public enum TileType { None, Wall, Room, Path, Door };