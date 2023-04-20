using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    public int timeSpeed = 1;

    int day = 1;
    float hour = 0;
    float time = 0;

    public Action dayChange;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime * timeSpeed;
        if(time >= 15)
        {
            time = 0;
            hour++;
        }
        if(hour >= 24)
        {
            hour = 0;
            day++;
            dayChange?.Invoke();
        }
        timerText.text = $"Day : {day} \nHour : {(int)hour}";
    }
}
