using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        isDead = true;
        StartCoroutine(WaitToDead(.3f));

    }

    IEnumerator WaitToDead(float delay)
    {
        EnemySpawner.Instance.KillEnemy(gameObject);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
