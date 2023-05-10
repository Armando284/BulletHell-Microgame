using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : EnemyAttack
{
    [SerializeField] private Transform[] shootPoints;

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

            foreach (Transform shootPoint in shootPoints)
            {
                GameObject bullet = Instantiate<GameObject>(bulletType, shootPoint.position, shootPoint.rotation);
                // The direction must be between the shootpoint right since this is a AOE
                bullet.GetComponent<Bullet>().direction = shootPoint.right;
                bullet.GetComponent<Bullet>().ownerTag = gameObject.tag;
            }
            yield return new WaitForSeconds(shootingRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        foreach (Transform shootPoint in shootPoints)
        {
            Debug.DrawLine(shootPoint.position, shootPoint.position + shootPoint.right * attackDistance, Color.blue);
        }
    }
}
