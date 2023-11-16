using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private static HealthManager instance;

    private int maxHealth = 3;
    private int currentHealth;
    public List<Sprite> heartSprites;
    public List<Image> hearts;

    void Awake()
    {
        instance = this;
    }

    public static HealthManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentHealth = 1;
    }

    public void adjustHealth()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = heartSprites[0];
            }
            else
            {
                hearts[i].sprite = heartSprites[1];
            }
        }
    }
    public void AddHealth(int deltaHealth)
    {
        currentHealth += deltaHealth;
        adjustHealth();
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(currentHealth == 0)
        {
            MapManager.GetInstance().GameOver();
        }
    }

    public void DoDamage(int damage)
    {
        currentHealth -= damage;
    }
}
