using System;
using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    [SerializeField] private int defaultMaxHealth = 3;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    void Awake()
    {
        MaxHealth = defaultMaxHealth;
        CurrentHealth = MaxHealth;
    }
    public void Initialize(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        DetectDie();
    }

    void DetectDie()
    {
        if (CurrentHealth <= 0) Destroy(gameObject);
    }
}
