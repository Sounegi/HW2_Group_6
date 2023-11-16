using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private int seconds;
    private int minutes;
    private float prevTime;
    private TextMeshProUGUI timerText;

    public bool timerActive;
    
    void Start()
    {
        timerActive = true;
        Reset();
        timerText = GetComponent<TextMeshProUGUI>();
        EnemyManager.GetInstance().OnEnd += StopTimer;
    }

    void Update()
    {
        if(timerActive)
        {
            float elapsedTime = Time.time - prevTime;
            if(elapsedTime % 60 > 1)
            {
                prevTime = Time.time;
                seconds += 1;
            }
            if(seconds > 59)
            {
                seconds = 0;
                minutes += 1;
            }
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void Reset()
    {
        prevTime = Time.time;
        seconds = 0;
        minutes = 0;
        timerActive = true;
    }

    public void StopTimer() {
        timerActive = false;
    }
}
