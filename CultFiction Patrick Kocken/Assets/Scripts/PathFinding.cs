using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    public List<Vector3> PathPositions;
    [SerializeField] private Grid _grid;
    public Vector3 FirstPathPos;
    public List<Node> Path;

    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        PathPositions.Clear();

        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].GetFCost() < node.GetFCost() || openSet[i].GetFCost() == node.GetFCost())
                {
                    if (openSet[i].GetFCost() < node.GetFCost())
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

        }
    }
    private void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        _grid.Path = path;

        for (int i = 0; i < path.Count; i++)
        {
            PathPositions.Add(path[i].WorldPosition);
        }
    }


    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.Grid.x - nodeB.Grid.x);
        int distanceY = Mathf.Abs(nodeA.Grid.y - nodeB.Grid.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
