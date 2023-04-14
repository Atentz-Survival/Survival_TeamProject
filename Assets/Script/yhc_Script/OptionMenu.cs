using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    Slider brightnessSlider;
    Image brightness;
    float bValue;

    private void Awake()
    {
        brightnessSlider = GetComponentInChildren<Slider>();
        brightness = GameObject.Find("Brightness").GetComponent<Image>();
    }

    private void Update()
    {
        bValue = brightnessSlider.value;
        // brightness.color.Equals = (0, 0, 0, bValue);
    }
}
