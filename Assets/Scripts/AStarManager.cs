using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AStarManager : MonoBehaviour
{
   public static AStarManager instance;
    private void Awake()
    {
        instance = this;
    }

    [System.Obsolete]
    public List<Node> GeneratePath(Node start, Node end)
    {
        Debug.Log($"Finding path from {start.name} to {end.name}");
        List<Node> openSet = new List<Node>();

        foreach(Node n in FindObjectsOfType<Node>())
        {
            n.gScore = float.MaxValue;
        }

        
        start.gScore = 0;
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);

        while(openSet.Count > 0)
        {
            int lowestF = default;

            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore())
                {
                    lowestF = i;
                }
            }

            Node currentNode = openSet[lowestF];
            openSet.Remove(currentNode);

            if (currentNode == end)
            {
                List<Node> path = new List<Node>(); 

                path.Insert(0, end);

                while (currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }

                path.Reverse();
                if (path == null)
                {
                    Debug.LogError("Pathfinding failed! No path was returned.");
                }
                else
                {
                    return path;
                }
            }


            foreach (Node connectedNode in currentNode.connections)
            {
                float tentativeGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (tentativeGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = tentativeGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);

                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }



        }

        return null;
    }

    public Node[] NodesInScene()
    {
        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);

        return nodes;
    }

    int ExtractNumber(string name)
    {
        string numberString = System.Text.RegularExpressions.Regex.Match(name, @"\d+").Value;
        return int.TryParse(numberString, out int result) ? result : 0;
    }
}
