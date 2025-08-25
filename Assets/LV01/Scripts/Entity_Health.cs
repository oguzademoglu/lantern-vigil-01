using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entity_VFX;
    private EntityBase entity;
    [SerializeField] protected float maxHp = 3;
    [SerializeField] protected bool isDead;
    [SerializeField] protected Vector2 knockbackVelocity;
    [SerializeField] protected float knockbackDuration;


    protected virtual void Awake()
    {
        entity = GetComponent<EntityBase>();
        entity_VFX = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(int damage, Transform damageDealer)
    {
        if (isDead) return;
        Vector2 knockbackPower = CalculateKnockback(damageDealer);
        entity?.ReceiveKnockback(knockbackPower, knockbackDuration);
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

    Vector2 CalculateKnockback(Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        knockbackVelocity.x *= direction;
        return knockbackVelocity;
    }
}
