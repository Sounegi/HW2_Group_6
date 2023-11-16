using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIncrement : MonoBehaviour
{
    private static ScoreIncrement instance;

    private int scoreValue;
    private TextMeshProUGUI scoreText;
    
    public bool toggler1 = true;
    private bool toggler2 = false;

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

    public static ScoreIncrement GetInstance()
    {
        return instance;
    }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int val)
    {
        scoreText.text = val.ToString();
    }
}
