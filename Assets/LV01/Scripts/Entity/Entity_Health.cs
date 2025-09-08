using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entity_VFX;
    private EntityBase entity;
    private Slider healthBar;
    private Entity_Stats stats;
    // [SerializeField] protected float maxHp = 3;
    [SerializeField] protected float currentHp;
    [SerializeField] protected bool isDead;
    [SerializeField] protected Vector2 knockbackVelocity;
    [SerializeField] protected float knockbackDuration;


    protected virtual void Awake()
    {
        entity = GetComponent<EntityBase>();
        entity_VFX = GetComponent<Entity_VFX>();
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();
        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
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
        currentHp -= damage;
        UpdateHealthBar();
        if (currentHp <= 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        healthBar.gameObject.SetActive(false);
        entity.EntityDeath();
        StartCoroutine(DeathCo());
        Debug.Log("Entity Died");
    }

    // void UpdateHealthBar() => healthBar.value = currentHp / maxHp;
    void UpdateHealthBar()
    {
        if (healthBar == null) return;
        healthBar.value = currentHp / stats.GetMaxHealth();
    }

    Vector2 CalculateKnockback(Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        knockbackVelocity.x *= direction;
        return knockbackVelocity;
    }

    IEnumerator DeathCo()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }
}
