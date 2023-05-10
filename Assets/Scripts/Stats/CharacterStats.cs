using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public Stat health;
    public float currentHealth;
    [SerializeField] private HealthBar healthBar;
    public Stat energy;
    public float currentEnergy;
    [SerializeField] private EnergyBar energyBar;
    public Stat speed;
    public Stat damage;
    public Stat armor;
    public float currentArmor;
    [SerializeField] private EnergyBar armorBar;

    [Space]
    public bool isDead = false;
    public bool isInvincible = false;
    public float invincibleOnHitTime = 1f;
    public bool canMove = true;
    public bool isHurt = false;

    public Animator animator;
    private Rigidbody2D rb;

    [Header("Wound Effects")]
    [SerializeField] private Transform bodyCenter;
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject[] bloodSplashes;
    [SerializeField] private SpriteRenderer[] renderers;
    [SerializeField] private Color[] originalColors;
    [SerializeField] private Color hurtColor;
    [SerializeField] private GameObject[] dropItems;

    private void Start()
    {
        SetHealth();
        currentHealth = health.GetValue();
        SetEnergy();
        currentEnergy = energy.GetValue();
        SetArmor();
        currentArmor = armor.GetValue();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[renderers.Length];
    }

    // Callback which is triggered when an stat changes.
    public delegate void OnStatChanged();
    public OnStatChanged onStatChangedCallback;

    public void SetHealth(float maxHealth = 0f)
    {
        if (maxHealth != 0f)
        {
            health.SetValue(maxHealth);
        }
        if (healthBar != null)
            healthBar.SetMaxHealth(health.GetValue());
        // Trigger callback
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void SetCurrentHealth(float currentHealth)
    {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, health.GetValue());
        if (healthBar != null)
            healthBar.SetHealth(this.currentHealth);
        // Trigger callback
        //if (onStatChangedCallback != null)
        //    onStatChangedCallback.Invoke();
    }

    public void SetEnergy(float maxEnergy = 0f)
    {
        if (maxEnergy != 0f)
        {
            energy.SetValue(maxEnergy);
        }
        if (energyBar != null)
            energyBar.SetMaxEnergy(energy.GetValue());
        // Trigger callback
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void SetCurrentEnergy(float currentEnergy)
    {
        this.currentEnergy = Mathf.Clamp(currentEnergy, 0, energy.GetValue());
        if (energyBar != null)
            energyBar.SetEnergy(this.currentEnergy);
        // Trigger callback
        //if (onStatChangedCallback != null)
        //    onStatChangedCallback.Invoke();
    }

    public void SetArmor(float maxArmor = 0f)
    {
        //if (maxEnergy != 0f)
        //{
        //    energy.SetValue(maxEnergy);
        //}
        if (armorBar != null)
            armorBar.SetMaxEnergy(armor.GetValue());
        // Trigger callback
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void SetCurrentArmor(float currentArmor)
    {
        this.currentArmor = Mathf.Clamp(currentArmor, 0, armor.GetValue());
        if (armorBar != null)
            armorBar.SetEnergy(this.currentArmor);
    }

    public bool HasMaxEnergy() => currentEnergy == energy.GetValue();

    public bool HasMaxArmor() => currentArmor == armor.GetValue();

    public void ApplyCure(float amount)
    {
        // TODO: Play curing animation
        SetCurrentHealth(currentHealth + amount);
    }

    public void ApplyDamage(DamageData data)
    {
        if (isInvincible)
            return;

        isHurt = true;
        float damage = data.damage;
        float armorDamage = damage;
        damage -= currentArmor;
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        SetCurrentArmor(Mathf.Clamp((currentArmor - armorDamage), 0, armor.GetValue()));
        Vector3 position = data.position;
        SetCurrentHealth(currentHealth - damage);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        Bleed();
        if (animator != null)
            animator.SetTrigger("Hit");
        Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
        rb.velocity = Vector2.zero;
        rb.AddForce(damageDir * 10);
        float STUN_TIME = .25f; // TODO: This should come from the weapon
        StartCoroutine(Stun(STUN_TIME));
        StartCoroutine(HurtTime(.1f));
        StartCoroutine(MakeInvincible(invincibleOnHitTime));
    }

    public void RecoverArmor(float armorGain)
    {
        SetCurrentArmor(currentArmor + armorGain);
    }

    public void RecoverEnergy(float energyGain)
    {
        SetCurrentEnergy(currentEnergy + energyGain);
    }

    public void SpendEnergy(float energySpent)
    {
        SetCurrentEnergy(currentEnergy - energySpent);
    }

    IEnumerator HurtTime(float time)
    {
        ChangeColors(hurtColor);
        yield return new WaitForSeconds(time);
        ChangeColors(Color.white, true);
        isHurt = false;
    }

    IEnumerator Stun(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    IEnumerator MakeInvincible(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        // TODO: Items must be droped here
        Debug.Log(transform.name + " died.");
        isHurt = false;
        isDead = true;
        LargeBleed();
    }

    private void DropItems()
    {
        if (dropItems.Length <= 0)
            return;

        foreach (GameObject item in dropItems)
        {
            GameObject newItem = Instantiate<GameObject>(item, transform.position + Vector3.up, Quaternion.identity);
            newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 1f), 1) * 4f, ForceMode2D.Impulse);
        }
    }

    private void ChangeColors(Color color, bool goBack = false)
    {
        if (renderers == null || renderers.Length <= 0)
            return;

        int count = 0;
        foreach (SpriteRenderer rend in renderers)
        {
            if (goBack)
            {
                rend.color = originalColors[count];
            }
            else
            {
                originalColors[count] = rend.color;
                rend.color = color;
            }
            count++;
        }
    }

    public void Bleed()
    {
        if (bloodParticles == null)
            return;

        Instantiate<GameObject>(bloodParticles, bodyCenter.position, Quaternion.identity);
    }

    public void LargeBleed()
    {
        if (bloodSplashes == null || bloodSplashes.Length <= 0)
            return;
        GameObject blood = Instantiate<GameObject>(bloodSplashes[Mathf.CeilToInt(Random.Range(0f, bloodSplashes.Length - 1))], transform.position, Quaternion.identity);
        blood.transform.parent = MapCreator.Instance.interactable.transform;
    }
}
