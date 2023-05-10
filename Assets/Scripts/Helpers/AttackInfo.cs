using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the attacks types common information
/// </summary>
[System.Serializable]
public class AttackInfo
{
    public bool canAttack = true;
    public float damageArea = 1f;
    public float damageMult = 1f;
    public float energyCost = 1f;
    public float cooldown = 1f;

    public AttackInfo(float _damageArea, float _damageMult, float _energyCost, float _cooldown)
    {
        this.damageArea = _damageArea;
        this.damageMult = _damageMult;
        this.energyCost = _energyCost;
        this.cooldown = _cooldown;
    }
}
