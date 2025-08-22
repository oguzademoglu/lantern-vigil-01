using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{

    EntityBase entity;
    void Awake()
    {
        entity = GetComponentInParent<EntityBase>();
    }

    public void CallTrig()
    {
        entity.CallAnimTrig();
    }
}
