using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entity_VFX;
    [SerializeField] protected float maxHp = 3;
    [SerializeField] protected bool isDead;


    void Awake()
    {
        entity_VFX = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(int damage, Transform damageDealer)
    {
        if (isDead) return;
        entity_VFX?.PlayOnDamageVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(int damage)
    {
        maxHp -= damage;
        if (maxHp <= 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log("Entity Died");
    }
}
