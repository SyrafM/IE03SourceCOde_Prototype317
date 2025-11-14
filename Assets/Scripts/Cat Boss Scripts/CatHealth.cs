using UnityEngine;

public class CatHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject UpperBarrier;
    public GameObject BackgroundMusic;
    public GameObject DoorClosed;
    public GameObject DoorOpen;

    public CatUI CatUI;

    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        CatUI.SetMaxHealth(maxHealth);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("meow meow feels hurt");
        currentHealth -= damage;
        CatUI.UpdateHealth(currentHealth);
        audioManager.PlaySFX(audioManager.bossHurt);


        if (currentHealth <= 0)
        {
            Destroy(gameObject);   // enemy is dead bruh
            Destroy(BackgroundMusic);
            UpperBarrier.SetActive(false);
            DoorClosed.SetActive(false);
            DoorOpen.SetActive(true);
        }
    }
}
