using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Sunshine : MonoBehaviour
{
    public Material skybox;
    float alpha = 0.0f;
    public float beta = 0.0008f;

    public float maxAlpha = 0.6f;
    public float forTimeInGame;  // 시간의 경과

    private bool isNight = false;

    public float vecT = 0.25f;
    float t;

    // fog
    private float morningFog;   // 아침의 안개량

    private void Start()
    {
        morningFog = RenderSettings.fogDensity;
    }

    private void Update()
    {
        SunLight();
    }

    void SunLight()
    {

        // [1] 목표 : 12분에 0 ~ 180도만큼 회전 -> 12분에 180 ~ 360도 회전 : 180/720 = 1/4 : 0.25
        t += Time.deltaTime;
        // transform.Rotate(new Vector3(forTimeInGame * t  , 0f , 0f)); // light의 회전
        transform.rotation = Quaternion.Euler(vecT * t , 0 , 0);  // 1초에 0.25도 만큼 회전
        Debug.Log(transform.rotation.x);


        if (transform.eulerAngles.x <= 10) //  0 <= x <= 170
        {
            isNight = false;                    // 낮이 된다.

            if (alpha <= 0.6f && alpha > -1.0f)
            {
                alpha += beta * forTimeInGame * t;
                skybox.SetFloat("_CubemapTransition", alpha);   // CubemapTrasition의 수치가 alpha가 된다. : 0 ~ 0.6
            }

            else if(transform.eulerAngles.x < 0.0f && transform.eulerAngles.x >170.0f)
            {
                alpha = maxAlpha;                               // 0.6
            }
        }

        else if (transform.eulerAngles.x >= 170)     // Light의 x값이 170보다 커지면
        {
            isNight = true;                     // 밤
        }



        if (isNight)                            // 밤이 되면
        {
            RenderSettings.fogDensity += 0.0001f;
            if(RenderSettings.fogDensity >= 0.7f)
            {
                RenderSettings.fogDensity = 0.7f;
            }

            skybox.SetFloat("_CubemapTransition", 0.7f);
        }

        else
        {
            RenderSettings.fogDensity = morningFog;            // 아침의 fog량은 0.0001로 고정.
        }
    }

}
