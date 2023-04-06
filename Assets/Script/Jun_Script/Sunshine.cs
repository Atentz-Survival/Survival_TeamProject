using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Sunshine : MonoBehaviour
{
    public delegate void ReSpawnObject();                  // 스폰을 위해 태양의 회전과 밤낮을 델리게이트로
    public static ReSpawnObject OnRespawn;                 // 다른 클래스에서 호출시키기 위한 변수설정


public Material skybox;
    float alpha = 0.0f;
    public float beta = 0.0008f;

    public float maxAlpha = 0.6f;
    public float forTimeInGame;  // 시간의 경과

    private bool isNight = false;

    public float vecT = 0.5f;    // 6분경과
    float t;
    float round;

    Quaternion vec;

    // fog
    private float morningFog;   // 아침의 안개량

    private void Start()
    {
        OnRespawn = SunLight;
        morningFog = RenderSettings.fogDensity;
    }

    private void Update()
    {
        SunLight();
    }

    void SunLight()
    {

        // [1] 목표 : 12분에 0 ~ 180도만큼 회전 -> 12분에 180 ~ 360도 회전 : 180/720 = 1/4 : 0.25
        // transform.Rotate(new Vector3(forTimeInGame * t  , 0f , 0f)); // light의 회전

        t += Time.deltaTime;
        round = vecT * t;
        vec = Quaternion.Euler(round, 0, 0);
        transform.rotation = vec;  // 1초에 0.25도 만큼 회전
        // Debug.Log(transform.rotation.x);


        if (transform.eulerAngles.x <= 10) //  0 <= x <= 170
        {
            isNight = false;                    // 낮이 된다.

            if (alpha < 0.6f && alpha > -0.1f)
            {
                alpha += beta * forTimeInGame * t;
                skybox.SetFloat("_CubemapTransition", alpha);   // CubemapTrasition의 수치가 alpha가 된다. : 0 ~ 0.6
            }

            else if(alpha >= maxAlpha)
            {
                alpha = maxAlpha;                               // 0.6
            }
        }

        else if (transform.eulerAngles.x < 0.0f && transform.eulerAngles.x >= 170)     // Light의 x값이 170보다 커지면
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
