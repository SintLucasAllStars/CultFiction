using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public LayerMask UnWalkableMask;
    public Vector2Int GridWorldSize;
    public float NodeRadius;

    public Vector3 WorldBottomLeft;

    private float NodeDiameter;
    private Vector2Int GridSize;

    private Node[,] _grid;

    public List<Node> Path;

    public void StartPathFinding()
    {
        NodeDiameter = NodeRadius * 2;
        GridSize = new Vector2Int(Mathf.RoundToInt(GridWorldSize.x / NodeDiameter), Mathf.RoundToInt(GridWorldSize.y / NodeDiameter));
        
        CreateGrid();
    }
    private void CreateGrid()
    {
        _grid = new Node[GridSize.x, GridSize.y];
        //WorldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.forward * (y * NodeDiameter + NodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnWalkableMask));
                _grid[x, y] = new Node(walkable, worldPoint, new Vector2Int(x, y));
            }
        }
    }
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.Grid.x + x;
				int checkY = node.Grid.y + y;

				if (checkX >= 0 && checkX  < GridSize.x && checkY >= 0 && checkY < GridSize.y) {
					neighbours.Add(_grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector2 percent = new Vector2((worldPosition.x + GridWorldSize.x / 2) / GridSize.x, (worldPosition.z + GridWorldSize.y / 2) / GridSize.y);
        percent = new Vector2(Mathf.Clamp01(percent.x), Mathf.Clamp01(percent.y));

        int x = Mathf.RoundToInt((GridSize.x - 1) * percent.x);
        int y = Mathf.RoundToInt((GridSize.y - 1) * percent.y);

        return _grid[x, y];
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR_WIN
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                if (Path != null)
                    if (Path.Contains(n))
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(n.WorldPosition, Vector3.one * (NodeDiameter - .1f));
                    }
            }
        }
        #endif
    }
}