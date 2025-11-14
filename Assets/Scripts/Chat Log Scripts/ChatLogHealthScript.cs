using UnityEngine;

public class ChatLogHealthScript : MonoBehaviour, IDamageable
{
    public int maxHealth = 2;
    private int currentHealth;
    
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
            Destroy(gameObject); // chatlog disappears
        }
    }
    
}
