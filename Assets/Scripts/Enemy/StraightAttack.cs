using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : EnemyAttack
{
    [SerializeField] private Transform shootPoint;

    public override void Attack()
    {
        base.Attack();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < continuosShoots; i++)
        {
            if (target == null) continue;

            GameObject bullet = Instantiate<GameObject>(bulletType, shootPoint.position, shootPoint.rotation);
            // The direction must be between the enemy and the player
            // instead of between the shootpoint and the player
            // to add an error margin so the game is not impossible
            bullet.GetComponent<Bullet>().direction = (target.position - transform.position).normalized;
            bullet.GetComponent<Bullet>().ownerTag = gameObject.tag;
            yield return new WaitForSeconds(shootingRate);
        }
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Debug.DrawLine(shootPoint.position, shootPoint.position + shootPoint.right * attackDistance, Color.blue);
    }
}
