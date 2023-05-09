using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool canAttack = true;
    public GameObject bulletType;
    public float attackDistance;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            Debug.Log("Distance: " + distance);
            if (distance <= attackDistance && canAttack)
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        canAttack = false;
        Debug.Log(name + " attack!");
        StartCoroutine(WaitToAttack(.3f));
    }

    IEnumerator WaitToAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}
