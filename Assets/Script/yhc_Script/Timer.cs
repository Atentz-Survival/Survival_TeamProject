using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    SaveFileUI save;
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
        save = FindObjectOfType<SaveFileUI>();
        timerText.text = $"Day : {day} \nHour : {hour}"; // 시계 초기값 1일차 6시로 설정
        sunshine.HourChange += OnHourChange;
        save.SaveFile += setData;
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
    private void setData()
    {
        DataController.Instance.gameData.currentDay = day;
        DataController.Instance.gameData.currentTime = hour;
    }
}