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

    public int day = 1;
    public int hour = 6;

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
    if (hour > 23)
    {
        day++;
        hour = 0;
    }
    timerText.text = $"Day : {day} \nHour : {hour}";
}

}