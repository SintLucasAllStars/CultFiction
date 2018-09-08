using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public LayerMask unWalkableMask;
    public Vector2Int gridWorldSize;
    public float nodeRadius;

    public Vector3 worldBottomLeft;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    Node[,] grid;

    public void StartPathFinding()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX  < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector2 percent;

        percent.x = (worldPosition.x + gridWorldSize.x / 2) / gridSizeX;
        percent.y = (worldPosition.z + gridWorldSize.y / 2) / gridSizeY;
        percent.x = Mathf.Clamp01(percent.x);
        percent.y = Mathf.Clamp01(percent.y);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percent.x);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percent.y);

        return grid[x, y];
    }
    public List<Node> path;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
                    }
            }
        }
    }
}