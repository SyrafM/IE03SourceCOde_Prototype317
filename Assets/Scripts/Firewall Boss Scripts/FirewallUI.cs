using UnityEngine;
using UnityEngine.UI;


public class FirewallUI : MonoBehaviour
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
}