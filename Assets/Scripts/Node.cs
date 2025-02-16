using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    public float gScore; //how many moves it takes to get to each node
    public float hScore; //estimate cost from current node to end node

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (connections.Count > 0)
        {
            for(int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }
}
