using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAI : EnemyMovementAI
{
    public override void MovementDecition()
    {
        base.MovementDecition();
        if (targetPosition != target.position)
        {
            targetPosition = target.position;
            if (distance < maxDistance)
            {
                moveTarget = targetPosition;
                GetComponent<EnemyPathfinding>().SetTargetPosition(targetPosition);
            }
        }
    }
}
