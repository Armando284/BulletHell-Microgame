using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : EnemyAttack
{
    [SerializeField] private Transform shootPoint;

    public override void Attack()
    {
        base.Attack();
        GameObject bullet = Instantiate<GameObject>(bulletType, shootPoint.position, shootPoint.rotation);
        // The direction must be between the enemy and the player
        // instead of between the shootpoint and the player
        // to add an error margin so the game is not impossible
        bullet.GetComponent<Bullet>().direction = (target.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().ownerTag = gameObject.tag;
    }
}
