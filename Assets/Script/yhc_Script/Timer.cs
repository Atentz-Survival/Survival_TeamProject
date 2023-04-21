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
    int hour = 6;

    public Action<int> dayChange;
    public Action<int> hourChange;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        sunshine = FindObjectOfType<Sunshine>();
    }

    private void Start()
    {
        sunshine.HourChange += OnHourChange;
    }

    private void OnHourChange()
    {
        hour = hour + 1;
        if(hour >23)
        {
            day++;
            hour =0;
        }
        timerText.text = $"Day : {day} \nHour : {hour}";
        dayChange?.Invoke(day);
        hourChange?.Invoke(hour);
    }

}
