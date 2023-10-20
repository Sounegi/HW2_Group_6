using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIncrement : MonoBehaviour
{
    private int scoreValue;
    private TextMeshProUGUI scoreText;
    
    public bool toggler1 = true;
    private bool toggler2 = false;

    void Start()
    {
        Reset();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(toggler1 == toggler2)
        {
            IncrementScore();
            toggler2 = !toggler2;
        }
    }

    public void IncrementScore(int val = 100)
    {
        scoreValue += val;
        scoreText.text = scoreValue.ToString();
    }

    public void Reset()
    {
        scoreValue = 0;
    }
}
