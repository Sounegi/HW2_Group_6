using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    private int currentScore;

    void Awake()
    {
        currentScore = 0;
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

     public static ScoreManager GetInstance()
    {
        return instance;
    }

    void Update()
    {
        if(ScoreIncrement.GetInstance() != null)
        {
            ScoreIncrement.GetInstance().SetScore(currentScore);
        }
    }

    public void Increment()
    {
        currentScore += 100;
    }
}
