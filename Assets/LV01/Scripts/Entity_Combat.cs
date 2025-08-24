using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public int damageAmount = 1;
    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private LayerMask targetLayer;

    public void PerformAttack()
    {
        Collider2D[] targetColliders = GetDetectedColliders();
        foreach (var target in targetColliders)
        {
            Entity_Health targetHealth = target.GetComponentInParent<Entity_Health>();
            targetHealth.TakeDamage(damageAmount);
        }
    }

    Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, targetLayer);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
