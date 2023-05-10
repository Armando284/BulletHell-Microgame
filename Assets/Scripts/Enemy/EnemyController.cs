using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats stats;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stats.isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            GetComponent<EnemyPathfinding>().SetTargetPosition(collision.transform.position);
            GetComponent<EnemyAttack>().target = collision.transform;
            GetComponent<EnemyMovementAI>().target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stats.isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            GetComponent<EnemyAttack>().target = null;
            GetComponent<EnemyMovementAI>().target = null;
        }
    }
}
