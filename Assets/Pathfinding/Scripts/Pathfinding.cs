﻿/* 
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
using System.Linq;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    private bool hasOneEntry = false;
    private bool hasOneExit = false;

    public Pathfinding(int width, int height, Vector3 position)
    {
        Instance = this;
        grid = new Grid<PathNode>(width, height, 10f, position, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

        int spawnObjectsCount = 0;

        #region LevelCreation
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode node = grid.GetGridObject(x, y);
                PathNode down = null;
                PathNode left = null;
                PathNode downLeft = null;

                if (y > 0)
                {
                    down = grid.GetGridObject(x, y - 1);
                }
                if (x > 0)
                {
                    left = grid.GetGridObject(x - 1, y);
                    if (y > 0)
                    {
                        downLeft = grid.GetGridObject(x - 1, y - 1);
                    }
                }


                FloorNodeType randomSeed;
                if (down == null && left == null && downLeft == null)
                {
                    randomSeed = MapCreator.Instance.floorNodes[Mathf.FloorToInt(Random.Range(0f, MapCreator.Instance.floorNodes.Count))].type;
                }
                else
                {
                    List<FloorNodeType> finalList = new List<FloorNodeType>();
                    List<FloorNodeType> downFloorNode = new List<FloorNodeType>();
                    if (down != null)
                    {
                        downFloorNode = MapCreator.Instance.floorNodes.Find((FloorNode obj) => obj.type == down.floorType).neighboorTypes;
                    }
                    List<FloorNodeType> leftFloorNode = new List<FloorNodeType>();
                    if (left != null)
                    {
                        leftFloorNode = MapCreator.Instance.floorNodes.Find((FloorNode obj) => obj.type == left.floorType).neighboorTypes;
                    }
                    List<FloorNodeType> downLeftFloorNode = new List<FloorNodeType>();
                    if (downLeft != null)
                    {
                        downLeftFloorNode = MapCreator.Instance.floorNodes.Find((FloorNode obj) => obj.type == downLeft.floorType).neighboorTypes;
                    }

                    if (downFloorNode.Count == 0 && leftFloorNode.Count == 0)
                    {
                        finalList = downLeftFloorNode;
                    }
                    else if (downFloorNode.Count == 0 && downLeftFloorNode.Count == 0)
                    {
                        finalList = leftFloorNode;
                    }
                    else if (downLeftFloorNode.Count == 0 && leftFloorNode.Count == 0)
                    {
                        finalList = downFloorNode;
                    }
                    else
                    {
                        finalList = downFloorNode.Intersect(leftFloorNode).Intersect(downLeftFloorNode).ToList();
                    }

                    int randomNeighboor = Mathf.FloorToInt(Random.Range(0f, finalList.Count()));
                    randomSeed = finalList.ElementAt(randomNeighboor);
                }
                FloorNodeType randomType = randomSeed;
                node.SetFloor(randomType);

                // Is Border
                if (x == 0 || x == grid.GetWidth() - 1 || y == 0 || y == grid.GetHeight() - 1)
                {
                    node.SetIsWalkable(false);
                    node.SetSpawn(EnvironmentNodeType.Wall);
                }
                else if (x == 1 && y == 1 && !hasOneEntry) // make shure to spawn entry at the right place
                {
                    hasOneEntry = true;
                    node.SetSpawn(EnvironmentNodeType.SceneEntry);
                }
                else
                {
                    FloorNode current = MapCreator.Instance.floorNodes.Find((FloorNode obj) => obj.type == randomType);

                    if (current.environmentTypes.Count > 0)
                    {
                        float spawnChance = Random.Range(0f, 1f);
                        // Spawning scene exit has bigger posibilities than other nodes,
                        // if it gets to the last cell without spawning then forcibly spawns
                        // only spawn once
                        if (spawnChance > .1f)
                        {
                            if ((x > grid.GetWidth() / 2 && y > grid.GetHeight() / 2 && !hasOneExit) || (x == grid.GetWidth() - 1 && y == grid.GetHeight() - 1 && !hasOneExit))
                            {
                                hasOneExit = true;
                                node.SetSpawn(EnvironmentNodeType.SceneExit);

                            }
                        }
                        else if (spawnObjectsCount < width * 2)
                        {
                            EnvironmentNodeType type;
                            do
                            {
                                int spawnType = Mathf.FloorToInt(Random.Range(0f, current.environmentTypes.Count - 1));
                                type = current.environmentTypes[spawnType];
                            } while (type == EnvironmentNodeType.SceneEntry || type == EnvironmentNodeType.SceneExit); // no other entry or exit

                            if (type == EnvironmentNodeType.Chest || type == EnvironmentNodeType.Rock)
                                node.SetIsWalkable(false);
                            spawnObjectsCount++;
                            node.SetSpawn(type);
                        }
                    }

                    // If node is not wall it can be an enemy spawner
                    // First quadrant is left for the player spawn
                    if (x > 5 || y > 5)
                    {
                        EnemySpawner.Instance.spawnPoints.Add(grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f);
                    }
                }
            }
        }
        #endregion

        #region EnemySpawn FirstWave

        EnemySpawner.Instance.enemyScale = grid.GetCellSize();
        EnemySpawner.Instance.SpawnWave();

        #endregion
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        if (startNode == null || endNode == null)
        {
            // Invalid Path
            return null;
        }

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        //PathfindingDebugStepVisual.Instance.ClearSnapshots();
        //PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, startNode, openList, closedList);

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // Reached final node
                //      PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                //    PathfindingDebugStepVisual.Instance.TakeSnapshotFinalPath(grid, CalculatePath(endNode));
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
                //PathfindingDebugStepVisual.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
