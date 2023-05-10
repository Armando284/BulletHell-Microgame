using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public GameObject throwableObject;
    public Transform attackCheck;
    private Rigidbody2D m_Rigidbody2D;
    private Animator animator;
    public bool isTimeToCheck = false;

    public AttackInfo mainAttack;

    private PlayerStats playerStats;
    [SerializeField] private LayerMask attackLayers;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        animator = PlayerController.Instance.animator;
    }

    private bool HasEnergy(float spence) => playerStats.currentEnergy >= spence;

    public void MainAttack()
    {
        if (Common.IsMouseOverUI())
            return;

        if (!mainAttack.canAttack || !HasEnergy(mainAttack.energyCost))
            return;

        playerStats.SpendEnergy(mainAttack.energyCost);
        mainAttack.canAttack = false;
        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackCombo", 0f);

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        mainAttack.canAttack = true;
    }

    public void DoDashDamage()
    {
        Debug.Log("DoDashDamage");

        DoDamage(mainAttack.damageArea, mainAttack.damageMult);
    }

    private void DoDamage(float damageArea = 1f, float damageMult = 1f)
    {
        Debug.Log("DoDamage");

        float dmgValue = Mathf.Abs(playerStats.damage.GetValue()) * damageMult;
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, damageArea, attackLayers);
        foreach (Collider2D collider in collidersEnemies)
        {
            Debug.Log("Attack: " + dmgValue);
            collider.gameObject.SendMessage("ApplyDamage", new DamageData(dmgValue, transform.position));
            CameraController.Instance.ShakeCamera(.5f, .1f);
        }
    }
}
