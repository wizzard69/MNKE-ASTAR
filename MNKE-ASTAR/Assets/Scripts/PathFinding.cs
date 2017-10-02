using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform seeker;
    public Transform target;

    grid _grid;

    private void Awake()
    {
        _grid = GetComponent<grid>();
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
    }

    int GetDistanceBetweenNodes(node nodeA, node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY= Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }

    void RetracePath(node startNode, node endNode)
    {
        List<node> path = new List<node>();
        node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        _grid.path = path;
    }


    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        node startNode = _grid.nodeFromWorldPoint(startPos);
        node endNode = _grid.nodeFromWorldPoint(endPos);

        List<node> openSet = new List<node>();
        HashSet<node> closedSet = new HashSet<node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            node currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost  == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (node neighbor in _grid.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistanceBetweenNodes(currentNode, neighbor);

                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistanceBetweenNodes(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
    }
}
