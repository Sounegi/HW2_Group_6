using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private static HealthManager instance;

    private int maxHealth = 3;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static HealthManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if(HeartManager.GetInstance() != null)
        {
            HeartManager.GetInstance().AdjustHealth(currentHealth);
        }
        
    }

    public void Reset()
    {
        currentHealth =  currentHealth;
    }

    public void AddHealth(int deltaHealth)
    {
        currentHealth += deltaHealth;
        // adjustHealth();
        
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
        Debug.Log("Health before " +  currentHealth.ToString());
        currentHealth -= damage;
        // HeartManager.GetInstance().AdjustHealth(currentHealth);
        Debug.Log("Health after " + currentHealth.ToString());

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            MapManager.GetInstance().GameOver();
        }
    }
}
