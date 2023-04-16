using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TimeManager timeManager;
    TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void FixedUpdate()
    {
        timerText.text = $"Day : {timeManager.Day}\n Time : {timeManager.Hour} : {timeManager.Minute}";
    }
}
