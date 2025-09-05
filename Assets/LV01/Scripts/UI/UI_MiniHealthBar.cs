using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    EntityBase Entity => GetComponentInParent<EntityBase>();

    void OnEnable()
    {
        Entity.OnFlipped += HandleFlip;
    }
    void OnDisable()
    {
        Entity.OnFlipped -= HandleFlip;
    }
    void HandleFlip() => transform.rotation = Quaternion.identity;


    // void Update()
    // {
    //     transform.rotation = Quaternion.identity;
    // }
}
