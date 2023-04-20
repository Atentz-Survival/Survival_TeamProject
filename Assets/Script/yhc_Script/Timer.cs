using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Sunshine sunshine;

    public int timeSpeed = 1;

    int day = 1;
    float hour = 0;
    float time = 0;

    public Action<int> dayChange;
    public Action<float> hourChange;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        sunshine = FindObjectOfType<Sunshine>();
    }

    private void Start()
    {
        sunshine.DayTimeDeli += onTimeChange;
    }

    private void onTimeChange(Quaternion quaternion)
    {
        hour = (float)sunshine.Vec.eulerAngles.magnitude * 15.0f;
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime * timeSpeed;
        if(time >= 15)
        {
            time = 0;
            hour++;
            hourChange?.Invoke(hour);
        }
        if(hour >= 24)
        {
            hour = 0;
            day++;
            dayChange?.Invoke(day);
        }
        timerText.text = $"Day : {day} \nHour : {(int)hour}";
    }
}
