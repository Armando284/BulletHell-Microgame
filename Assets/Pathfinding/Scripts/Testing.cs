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
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour
{

    //[SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    //[SerializeField] private PathfindingVisual pathfindingVisual;
    //[SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding pathfinding;
    [SerializeField] private LayerMask obstacleLayers;

    private void Start()
    {
        pathfinding = new Pathfinding(25, 25, Vector3.zero);

        //pathfinding = new Pathfinding(10, 10, new Vector3(120, 0, 0));

        //pathfinding = new Pathfinding(10, 10, new Vector3(-120, 0, 0));

        //pathfinding = new Pathfinding(10, 10, new Vector3(0, 120, 0));
        //pathfinding = new Pathfinding(10, 10, new Vector3(120, 120, 0));
        //pathfinding = new Pathfinding(10, 10, new Vector3(-120, 120, 0));

        //pathfinding = new Pathfinding(10, 10, new Vector3(0, -120, 0));
        //pathfinding = new Pathfinding(10, 10, new Vector3(120, -120, 0));
        //pathfinding = new Pathfinding(10, 10, new Vector3(-120, -120, 0));

        //pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        //pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        //    pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
        //    List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
        //    if (path != null)
        //    {
        //        for (int i = 0; i < path.Count - 1; i++)
        //        {
        //            Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
        //        }
        //    }
        //    characterPathfinding.SetTargetPosition(mouseWorldPosition);
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        //    pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
        //    pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        //}

        //for (int x = 0; x < pathfinding.GetGrid().GetWidth(); x++)
        //{
        //    for (int y = 0; y < pathfinding.GetGrid().GetHeight(); y++)
        //    {
        //        bool isWalkable = true;
        //        Vector3 position = pathfinding.GetGrid().GetWorldPosition(x, y) + Vector3.one * 5f;
        //        Debug.Log("pos: " + position);
        //        Collider2D[] obstacles = Physics2D.OverlapCircleAll(position, 1f, obstacleLayers);
        //        if (obstacles.Length > 0)
        //        {
        //            isWalkable = false;
        //            UtilsClass.CreateWorldText("F", null, position, 20, Color.red, TextAnchor.MiddleCenter);
        //        }

        //        pathfinding.GetNode(x, y).isWalkable = isWalkable;
        //    }
        //}
    }

}
