using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sunshine : MonoBehaviour
{
    public delegate void ReSpawnObject();                  // 스폰을 위해 태양의 회전과 밤낮을 델리게이트로
    public static ReSpawnObject OnRespawn;                 // 이 클래스내의 함수를 OnRespawn변수로 참조

    public Quaternion Vec
    {
        get => vec;
        set
        {
            if(vec != value)
            {
                vec = value;
                DayTimeDeli?.Invoke(vec);
            }
        }
    }

    public Action<Quaternion> DayTimeDeli;

    public Material skybox;
    float alpha = 0.0f;
    public float beta = 0.0008f;

    public float maxAlpha = 0.6f;
    public float forTimeInGame;  // 시간의 경과

    public bool isNight = false;

    public Action HourChange;

    public float vecT = 0.5f;    // 6분경과 : 0.3333f
    float t;
    float round;

    Quaternion vec;
    SaveBoardUI pauseMenu;
    // fog
    private float morningFog;   // 아침의 안개량

    //15도마다 델리게이트를 송신하기 위한 체크용 변수.
    int currentRound = 0;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<SaveBoardUI>();
    }

    private void Start()
    {
        // OnRespawn = SunLight;
        OnRespawn = SunRotate;
        // instance = this;
        RenderSettings.fogDensity = morningFog;
        pauseMenu.updateData += SetData;
    }

    private void Update()
    {
        OnRespawn?.Invoke();
        OnRespawn();
        SunRotate();
    }

    public void SunRotate()
    {
        // [1] 목표 : 12분에 0 ~ 180도만큼 회전 -> 12분에 180 ~ 360도 회전 : 180/720 = 1/4 : 0.25
        // transform.Rotate(new Vector3(forTimeInGame * t  , 0f , 0f)); // light의 회전

        t += Time.deltaTime;
        round = vecT * t;
        vec = Quaternion.Euler(round, 0, 0);
        
        

        if( (int)(round%15)==0 && currentRound != (int)round && (int)round !=360)
        {
            //델리게이트
            currentRound = (int)round;
            Debug.Log("SunRotate에서 15도 초과로 인한 델리게이트 발생");
            HourChange?.Invoke();
        }
        
        transform.rotation = vec;  // 1초에 0.25도 만큼 회전
                                   // Debug.Log(transform.rotation.x);
        DayTimeDeli?.Invoke(vec);
        // 로테이션값을 시간처럼 시각화하기
        if (round > 360f)
        {
            t = 0f;
        }
        // Debug.Log(vec);

        if ((vec.x >= 0 && vec.x <= 0.0001f) || (vec.x >= -0.0001f && vec.x <= 0)) //  0 <= x <= 170
        {
            RenderSettings.fogDensity = morningFog;             // 아침의 fog량은 0.0001로 고정.
            isNight = false;                    // 낮이 된다.
        }

        else if ((vec.x >=0.9999f && vec.x <= 1.0f)||(vec.x <= -0.9999f && vec.x >= -1.0f))     // Light의 x값이 170보다 커지면
        {
            isNight = true;                     // 밤
        }



        if (isNight)                            // 밤이 되면
        {
            RenderSettings.fogDensity += 0.000004f;              // 여기 조정!!!!!!!!!!!!!!!!!!
            if (RenderSettings.fogDensity >= 0.1f)              // 여기 조정!!!!!!!!!!!!!!!!!!
            {
                RenderSettings.fogDensity = 0.1f;
            }
            
            if(alpha >= 0.6f && alpha < 0.8f)
            {
                alpha += beta * forTimeInGame;
                skybox.SetFloat("_CubemapTransition", alpha);
            }
            else
            {
                skybox.SetFloat("_CubemapTransition", 0.8f);
            } 
        }

        else
        {
            if (alpha < 0.6f && alpha >= 0.0f)      // -0.1~
            {
                alpha += beta * forTimeInGame;              // 여기 조정!!!!!!!!!!!!!!!!!!
                skybox.SetFloat("_CubemapTransition", alpha);   // CubemapTrasition의 수치가 alpha가 된다. : 0 ~ 0.6
            }

            else if (alpha >= maxAlpha&& alpha < 0.7f)
            {
                alpha = maxAlpha;                               // 0.6
            }
            else
            {
                alpha = 0.0f;
            }
        }

    }

    public void SetData()
    {
        DataController.Instance.gameData.currentSunRotate = round;
    }
    // 텐트와 상호작용시 할 내용
    // 텐트와 상호작용시 -> 로딩 씬 출현 -> 다시 씬을 불러들여와서 쿼터니언의 범위 값이 0이되게한다.
    // get set을 이용하면 되지않을까....?

    // 04/10 씬 생성완료 씬을 설정해서 텐트와 상호작용 시 로딩 씬출력 텍스트 필요
}
