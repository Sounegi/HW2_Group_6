using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    private static HeartManager instance;

    public List<Sprite> heartSprites;
    public List<Image> hearts;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static HeartManager GetInstance()
    {
        return instance;
    }

    public void AdjustHealth(int currentHealth)
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
}
