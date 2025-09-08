using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{

    EntityBase entity;
    Entity_Combat entityCombat;
    void Awake()
    {
        entity = GetComponentInParent<EntityBase>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    public void CallTrig()
    {
        entity.CallAnimTrig();
    }

    public void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }
}
