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

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime * timeSpeed;
        if(time >= 15)
        {
            hour++;
            time = 0;
        }
        if(hour >= 24)
        {
            day++;
            hour = 0;
        }
        timerText.text = $"Day : {day} \nHour : {(int)hour}";
    }
}
