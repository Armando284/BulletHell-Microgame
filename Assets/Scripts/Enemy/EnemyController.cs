using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 currentTargetPosition;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            GetComponent<EnemyPathfiningMovement>().SetTargetPosition(collision.transform.position);
            GetComponent<EnemyAttack>().target = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position != currentTargetPosition)
            {
                GetComponent<EnemyPathfiningMovement>().SetTargetPosition(collision.transform.position);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDead)
            return;
        if (collision.CompareTag("Player"))
        {
            GetComponent<EnemyAttack>().target = null;
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
