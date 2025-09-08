using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    // public Stat maxHealth;
    public Stat_DefenceGroup defence;
    public Stat_OffenseGroup offense;
    public Stat_MajorGroup major;
    // public Stat vitality;


    public float GetMaxHealth()
    {
        float baseHp = defence.maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;
        return baseHp + bonusHp;
    }
}
