using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;

    private SpriteRenderer spriteRenderer;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHealth(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    /**
     * old code, keeping just in case idk
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CatBehaviour>(out CatBehaviour cat))
        {
            // for now, just hardcode the damage or give CatBehaviour a public int damage
            TakeDamage(cat.damage);
            StartCoroutine(FlashRed());
        }
    }
    **/

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHealth(currentHealth);
        StartCoroutine(FlashRed());
        audioManager.PlaySFX(audioManager.playerHurt);

        if (currentHealth <= 0)
        {
            // audioManager.PlaySFX(audioManager.playerDeath);
            //Destroy(gameObject);   // player is dead bruh
            // here is where you insert the game over screen or trigger whatever thing you want to happen when the player dies.
            StopAllCoroutines();
            GameStateManagerScript.instance.gameOver();
        }
    }

    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
        yield return new WaitForSeconds(0.2f);
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}