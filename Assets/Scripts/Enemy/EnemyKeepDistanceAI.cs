using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeepDistanceAI : EnemyMovementAI
{
    public override void MovementDecition()
    {
        base.MovementDecition();
        if (targetPosition != target.position)
        {
            targetPosition = target.position;
            if (distance < maxDistance && distance > minDistance)
            {
                moveTarget = targetPosition;
            }
            else if (distance <= minDistance)
            {
                moveTarget = transform.position + (targetPosition - transform.position).normalized * -30f;
            }
            GetComponent<EnemyPathfinding>().SetTargetPosition(moveTarget);
        }
    }


}