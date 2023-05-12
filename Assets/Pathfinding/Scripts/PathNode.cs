/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using CodeMonkey.Utils;

public class PathNode
{

    private Grid<PathNode> grid;
    public int x;
    public int y;
    public FloorNodeType floorType;
    public GameObject floorObject;
    private EnvironmentNodeType spawnType;
    public GameObject spawnObject;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
        Vector3 position = grid.GetWorldPosition(x, y) + Vector3.one * 5f;
        UtilsClass.CreateWorldText("F", null, position, 20, Color.red, TextAnchor.MiddleCenter);
    }

    public override string ToString()
    {
        return x + "," + y;
    }

    public void SetFloor(FloorNodeType type)
    {
        floorType = type;
        if (floorObject != null)
        {
            MapCreator.Instance.DestroyNode(floorObject);
            floorObject = null;
        }
        floorObject = MapCreator.Instance.CreateFloorNode(grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, floorType, grid.GetCellSize());
    }

    public void SetSpawn(EnvironmentNodeType type)
    {
        spawnType = type;
        if (spawnObject != null)
        {
            MapCreator.Instance.DestroyNode(spawnObject);
            spawnObject = null;
        }
        spawnObject = MapCreator.Instance.CreateSpawnNode(grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f, spawnType, grid.GetCellSize());
        SortingGroup group = spawnObject.AddComponent<SortingGroup>();
        group.sortingOrder = grid.GetHeight() - y;
    }
}
