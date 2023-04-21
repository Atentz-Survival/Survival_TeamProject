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
    public int Day
    {
        get { return day; }
    }

    float hour = 0;
    float minute = 0;
    float preMinute;
    float preHour;

    bool ison = true;

    WaitForSeconds wait = new WaitForSeconds(1);

    public Action<int> dayChange;

    // public Action<float> hourChange;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        sunshine = FindObjectOfType<Sunshine>();
    }

    private void Start()
    {
        sunshine.DayTimeDeli += onTimeChange; // 1초 60번
    }

    IEnumerator OnChange()
    {
        yield return wait;
        ison = true;
    }

    private void onTimeChange(Quaternion quaternion)
    {
        if (ison)
        {
            ison= false;
            hour = sunshine.Vec.eulerAngles.magnitude / 15;
            minute = sunshine.Vec.eulerAngles.magnitude % 15;
            Debug.Log(minute);
            
            dayChange?.Invoke(day);

            timerText.text = $"Day : {day} \nHour : {(int)hour}";
            StopAllCoroutines();
            StartCoroutine(OnChange());
        }
    }

}
