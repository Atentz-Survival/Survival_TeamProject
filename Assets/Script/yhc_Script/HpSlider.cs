using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    public Slider hpBar;

    private void Update()
    {
        if (hpBar.value < 1)
        {
            transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
        
    }
}
