using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class HealthUI : MonoBehaviour
{
    public Image healthBarImage;
    public Sprite[] healthSprites;

    private int maxHealth;

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        UpdateHealth(max);
    }

    public void UpdateHealth(int currentHealth)
    {
        int i = Mathf.Clamp(currentHealth, 0, maxHealth);   // this prevents the index from going out of the range of 0 to the max health
        Debug.Log($"Health index: {i}, MaxHealth: {maxHealth}");

        healthBarImage.sprite = healthSprites[i];
    }
    /*
    public Image heartPrefab;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private List<Image> hearts = new List<Image>();

    public void SetMaxHearts(int maxHearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(heartPrefab, transform);
            newHeart.sprite = fullHeartSprite;
            newHeart.color = Color.red;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeartSprite;
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
                hearts[i].color = Color.white;
            }
        }
    }
    */
}