using UnityEngine;

public class Pathfind
{
    private GenericGrid<PathNode> grid;

    public Pathfind(int width, int height)
    {
        grid = new GenericGrid<PathNode>(width, height, 10f, Vector3.zero, (GenericGrid<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
    }
}