using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunshine : MonoBehaviour
{
    public Material skybox;
    float alpha = 0.0f;
    public float beta = 0.00278f;
    float zeta;

    float maxAlpha = 1.0f;
    public float forTimeInGame;  // 시간의 경과

    private bool isNight = false;

    // fog
    private float morningFog;   // 아침의 안개량
    public float nightFog;      // 밤이될때의 fog량
    public float turnCalc;      // 안개의 증가량
    private float density;      // 안개의 총량

    private void Start()
    {
        morningFog = RenderSettings.fogDensity;
    }

    private void Update()
    {
        SunLight();

        CubeTransitionPoint();
    }

    void SunLight()
    {
        transform.Rotate(Vector3.right, forTimeInGame * Time.deltaTime); // light의 회전


        if (transform.eulerAngles.x >= 170)     // Light의 x값이 170보다 커지면
        {
            isNight = true;                     // 밤
            Debug.Log("2");
        }
        else if (transform.eulerAngles.x <= 10) // 해가 뜰때 ,
        {
            isNight = false;                    // 낮이 된다.
            Debug.Log("1");
        }

        if (isNight)                            // 밤이 되면
        {
            if (density <= nightFog)
            {
                density += turnCalc * Time.deltaTime;   // 특정 시간만큼 fog 증가
                RenderSettings.fogDensity = density;    // 새팅의 fogDensity가 density가된다.
                Debug.Log(density);
            }
        }
    }


    private void CubeTransitionPoint()
    {
        // CubemapTrasition의 값을 서서히 증가
        if (alpha <= maxAlpha)                               // CubemapTrasition가 1.0f보다 작거나 같을경우
        {
            zeta = beta * forTimeInGame * Time.deltaTime;                   // beta * Light의 회전량
            alpha += zeta;                                  // CubemapTrasition이 zeta만큼 계속증가
            skybox.SetFloat("_CubemapTransition", alpha);   // CubemapTrasition의 수치가 alpha가 된다.
        }

        else
        {
            alpha = 0.0f;                                   // CubemapTrasition의 값이 1.0f 보다 클경우 alpha값을 초기화
            // alpha값을 천천히 내리는 것도 생각중이지만 목표로하고있는것이
            // 낮-> 밤 되기 x초전 경고 -> x초후 바로바뀜 -> 텐트를 상호작용시 , CubemapTrasition의 값이 interval후에 0 이되게한다.

        }
    }

}
