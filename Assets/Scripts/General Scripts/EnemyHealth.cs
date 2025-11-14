using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 3;
    private int currentHealth;
    // public GameObject ExitDoor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);   // enemy is dead bruh
        }
    }
}