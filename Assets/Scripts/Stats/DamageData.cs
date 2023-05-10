using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    public float damage;
    public Vector3 position;

    public DamageData(float damage, Vector3 position)
    {
        this.damage = damage;
        this.position = position;
    }
}
