using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isDead = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
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
        if (isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            GetComponent<EnemyAttack>().target = null;
            GetComponent<EnemyMovementAI>().target = null;
        }
    }

    public void KillEnemy()
    {
        isDead = true;
        StartCoroutine(Die(.3f));
    }

    IEnumerator Die(float delay)
    {
        EnemySpawner.Instance.KillEnemy(gameObject);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
