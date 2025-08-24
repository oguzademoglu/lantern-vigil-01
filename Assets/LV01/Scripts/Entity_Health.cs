using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHp = 3;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;
        ReduceHp(damage);
    }

    protected void ReduceHp(int damage)
    {
        maxHp -= damage;
        if (maxHp <= 0) Die();
    }

    protected void Die()
    {
        isDead = true;
        Debug.Log("Entity Died");
    }
}
