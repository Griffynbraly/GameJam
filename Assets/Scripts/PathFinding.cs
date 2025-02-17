using UnityEngine;
using System.Collections.Generic;

public class PathFinding: MonoBehaviour 
{

    private Grid<PathNode> grid;

    public PathFinding(int width, int height)
    {
        grid = new Grid<PathNode>(width, height, 1f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }
}
