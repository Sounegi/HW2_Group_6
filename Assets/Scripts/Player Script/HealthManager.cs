using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    
    public List<Sprite> heartSprites;
    public List<Image> hearts;

    public bool toggler1 = true;
    private bool toggler2 = false;
    public bool type = true;

    void Start()
    {
        currentHealth = 3;
    }

    void Update()
    {
        if(toggler1 == toggler2)
        {
            adjustHealth(type);
            toggler2 = !toggler2;
        }
    }

    public void adjustHealth(bool type)
    {
        if(type && currentHealth > 0)
        {
            currentHealth -= 1;
        }
        else if(!type && currentHealth < maxHealth)
        {
            currentHealth += 1;
        }

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
}
