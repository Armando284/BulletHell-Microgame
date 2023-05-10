using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class EnemyPathfinding : MonoBehaviour
{
    public float speed = 40f;

    public int currentPathIndex;
    public List<Vector3> pathVectorList;
    public bool lookingRight = true;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        // HACK: Test pathfinding
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SetTargetPosition(UtilsClass.GetMouseWorldPosition());
        //}
    }

    private void HandleMovement()
    {
        if (!GetComponent<EnemyAttack>().canMove)
            return;

        if (pathVectorList != null && pathVectorList.Count > 0
            )
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                if (targetPosition.x < transform.position.x && lookingRight || targetPosition.x > transform.position.x && !lookingRight)
                {
                    Flip();
                }
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    public void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0, 180, 0);
    }
}
