using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool Walkable;
    public Vector3 WorldPosition;

    public int GCost;
    public int HCost;

    public Vector2Int Grid;

    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, Vector2Int _grid)
    {
        Walkable = _walkable;
        WorldPosition = _worldPos;
        Grid = _grid;
    }
    public int GetFCost()
    {
        return GCost + HCost;
    }
}