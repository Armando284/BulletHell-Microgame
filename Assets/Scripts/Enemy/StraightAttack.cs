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
        bullet.GetComponent<Bullet>().direction = (target.position - shootPoint.position).normalized;
        bullet.GetComponent<Bullet>().ownerTag = gameObject.tag;
    }
}
