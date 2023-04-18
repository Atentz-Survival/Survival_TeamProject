using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    int day = 1;
    float hour = 0;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        hour += Time.fixedDeltaTime;
        if(hour > 24)
        {
            day++;
            hour = 0;
        }
        timerText.text = $"Day : {day} \nHour : {(int)hour}";
    }
}
