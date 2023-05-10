using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
    //}

    //public void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    //{
    //    if (newItem != null)
    //    {
    //        armor.AddModifier(newItem.armorModifier);
    //        damage.AddModifier(newItem.damageModifier);
    //    }
    //    if (oldItem != null)
    //    {
    //        armor.RemoveModifier(oldItem.armorModifier);
    //        damage.RemoveModifier(oldItem.damageModifier);
    //    }
    //    // Trigger callback
    //    if (onStatChangedCallback != null)
    //        onStatChangedCallback.Invoke();
    //}

    public override void Die()
    {
        base.Die();
        StartCoroutine(WaitToDead());
    }

    IEnumerator WaitToDead()
    {
        // TODO: Change collider configuration to addapt to new body shape
        // CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        //capsule.offset = new Vector2(1.2f, 0.4f);
        //capsule.size = new Vector2(3f, 0.5f);
        //capsule.direction = CapsuleDirection2D.Horizontal;
        if (animator != null)
            animator.SetBool("IsDead", true);
        canMove = false;
        isInvincible = true;
        GetComponent<Attack>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        //m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
        if (animator != null)
            animator.SetBool("IsDead", false);
        yield return new WaitForSeconds(2f);
        HUDManager.Instance.gameEnd.GameOver();
        //GameManager.Instance.SceneChange("Level1", EntryName.RespawnStatue); // TODO here I could save the name of the last scene where I save which has a respawnStatue and use it instead of Town
    }
}
