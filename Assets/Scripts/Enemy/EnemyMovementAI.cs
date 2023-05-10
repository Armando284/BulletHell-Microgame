using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour
{
    public Transform target;
    public Vector3 targetPosition;
    public Vector3 moveTarget;
    public float distance;
    public float maxDistance = 30f;
    public float minDistance = 20f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
            MovementDecition();
        }
    }

    public virtual void MovementDecition()
    {

    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, moveTarget, Color.red);
    }
}
