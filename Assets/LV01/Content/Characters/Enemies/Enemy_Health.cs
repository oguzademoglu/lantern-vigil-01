using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Enemy_Health : Entity_Health
{
    Enemy Enemy => GetComponent<Enemy>();
    public override void TakeDamage(int damage, Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>() != null)
            Enemy.TryEnterBattleState(damageDealer);

        base.TakeDamage(damage, damageDealer);

        Debug.Log(maxHp);
    }
}
