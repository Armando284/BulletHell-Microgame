using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool canAttack = true;
    public GameObject bulletType;
    public float attackDistance;
    public Transform target;
    public bool canMove = true; // TODO: pass this to enemyStats
    public float attackCooldown = .3f;
    [Range(1, 10)] public int continuosShoots = 1;
    public float shootingRate = .3f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackDistance && canAttack)
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        canAttack = false;
        canMove = false;
        StartCoroutine(WaitToAttack(attackCooldown));
    }

    IEnumerator WaitToAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
