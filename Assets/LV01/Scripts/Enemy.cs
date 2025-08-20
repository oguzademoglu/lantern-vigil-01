using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private SimpleHealth health;
    void Awake()
    {
        health = GetComponent<SimpleHealth>();
    }
    void Start()
    {
        health.Initialize(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
