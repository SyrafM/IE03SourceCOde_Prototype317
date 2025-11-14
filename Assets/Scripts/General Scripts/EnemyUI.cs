using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class EnemyUI : MonoBehaviour
{
    public Image firePrefab;
    public Sprite fullFireSprite;
    public Sprite emptyFireSprite;

    private List<Image> fires = new List<Image>();

    public void SetMaxFires(int maxFires)
    {
        foreach (Image fire in fires)
        {
            Destroy(fire.gameObject);
        }

        fires.Clear();

        for (int i = 0; i < maxFires; i++)
        {
            Image newFire = Instantiate(firePrefab, transform);
            newFire.sprite = fullFireSprite;
            newFire.color = Color.red;
            fires.Add(newFire);
        }
    }

    public void UpdateFires(int currentHealth)
    {
        for (int i = 0; i < fires.Count; i++)
        {
            if (i < currentHealth)
            {
                fires[i].sprite = fullFireSprite;
                fires[i].color = Color.red;
            }
            else
            {
                fires[i].sprite = emptyFireSprite;
                fires[i].color = Color.white;
            }
        }
    }
}