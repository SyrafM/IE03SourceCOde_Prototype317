using UnityEngine;

public class FirewallHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int currentHealth;
    public GameObject enemyHandler;
    public GameObject HoleSpawner;
    public GameObject BackgroundMusic;

    public FirewallUI FirewallUI;
    public GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        FirewallUI.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth < 4)
        {
            HoleSpawner.SetActive(true);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FirewallUI.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            door.SetActive(true);
            Destroy(enemyHandler);   // enemy is dead bruh
            Destroy(BackgroundMusic);

        }
    }
}
