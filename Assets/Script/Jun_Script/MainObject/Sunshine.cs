using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.ProBuilder;
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
    public Action HourChange;

    // CubemapTransition 관련 변수 --------------------------------------------------------
    public Material skybox;
    float alpha = 0.0f;
    public float beta = 0.5f;
    private float gamma = 0.5f;
    public float maxAlpha = 0.6f;

    // 낮밤 확인 변수
    public bool isNight = false;

    // Sunshine의 회전값을 나타내는 변수 (Update)--------------------------------------------
    public float vecT = 0.5f;    // 6분경과 : 0.3333f
    float t;
    float round;

    // 기타-------------------------------------------------------------------------------
    Quaternion vec;
    SaveBoardUI pauseMenu;
    // fog
    private float morningFog;   // 아침의 안개량

    //15도마다 델리게이트를 송신하기 위한 체크용 변수.
    int currentRound = 0;


    // Sunshine의 회전값을 나타내는 변수 (FixedUpdate) --------------------------------------
    float rec = 1.0f;
    public float rotationAmount;


    private void Awake()
    {
        pauseMenu = FindObjectOfType<SaveBoardUI>();
    }

    private void Start()
    {
        rotationAmount = 0.0f;

        //HourChange += RoundTime;                        // 여기서 선샤인값을 받는다.
        RenderSettings.fogDensity = morningFog;

        pauseMenu.updateData += SetData;
        if (DataController.Instance.WasSaved == false)
        {
            PreInitialize();
        }
        else
        {
            Initialize();
        }
    }

    private void Update()
    {
        OnRespawn?.Invoke();
        //OnRespawn();
        SunRotate();
        SetFog();
    }

    private void FixedUpdate()
    {
        RotateFixed();
    }

    public void RotateFixed()
    {
        rotationAmount += rec * Time.fixedDeltaTime;
        Quaternion deltaQua = Quaternion.Euler(rotationAmount, 0, 0);
        transform.rotation = deltaQua;

        if ((int)(rotationAmount % 15) == 0 && currentRound != (int)rotationAmount && (int)rotationAmount != 360)
        {
            //델리게이트
            currentRound = (int)rotationAmount;
            Debug.Log("SunRotate에서 15도 초과로 인한 델리게이트 발생");
            HourChange?.Invoke();
        }
    }
    public void SunRotate()
    {
        // [1] 목표 : 12분에 0 ~ 180도만큼 회전 -> 12분에 180 ~ 360도 회전 : 180/720 = 1/4 : 0.25
        // transform.Rotate(new Vector3(forTimeInGame * t  , 0f , 0f)); // light의 회전

        t += Time.deltaTime;
        round = vecT * t;
        vec = Quaternion.Euler(round, 0, 0);

        // transform.rotation = vec;  // 1초에 0.25도 만큼 회전
                                   // Debug.Log(transform.rotation.x);
        DayTimeDeli?.Invoke(vec);
        // 로테이션값을 시간처럼 시각화하기
        if (round > 360f)
        {
            t = 0f;
        }
    }

    public void SetFog()
    {
        Vector3 eulerAngle = transform.rotation.eulerAngles;
        //Debug.Log(eulerAngle);
        if (eulerAngle.x <= 180.0f && eulerAngle.x >= 0.0f) //  0 <= x <= 170
        {
            RenderSettings.fogDensity = morningFog;             // 아침의 fog량은 0.0001로 고정.
            isNight = false;                    // 낮이 된다.
        }

        else if ((eulerAngle.x >180.0f && eulerAngle.x <=360.0f)||(eulerAngle.x < 0.0f))     // Light의 x값이 170보다 커지면
        {
            isNight = true;                     // 밤
        }



        if (isNight)                            // 밤이 되면
        {
            RenderSettings.fogDensity += 0.000004f;
            if (RenderSettings.fogDensity >= 0.1f) 
            {
                RenderSettings.fogDensity = 0.1f;
            }

            if (alpha >= 0.6f && alpha < 0.8f)
            {
                alpha += beta* gamma * Time.deltaTime;
                skybox.SetFloat("_CubemapTransition", alpha);
            }
            else
            {
                alpha = 0.8f;
                skybox.SetFloat("_CubemapTransition", alpha);
            }
        }

        else
        {
            if (alpha < 0.6f && alpha >= 0.0f)      // -0.1~
            {
                alpha += beta * gamma * Time.deltaTime;
                skybox.SetFloat("_CubemapTransition", alpha);   // CubemapTrasition의 수치가 alpha가 된다. : 0 ~ 0.6
            }

            else if (alpha >= maxAlpha && alpha < 0.7f)
            {
                alpha = maxAlpha;                               // 0.6
            }
            else
            {
                alpha = 0.0f;
            }
        }
    }

    // ------------------------- 04/26 float alpha 의 값과 float.RenderSettings.fogDensity 의 값을 세이브할 필요가 있을거 같습니다---------------------------------------
    // float alpha 는 Cubemap Transition의 수치이며, alpha를 저장하면 하늘의 색을 저장할 수 있다.
    // float.RenderSettings.fogDensity 는 Fog의 수치이며, 밤이 되었을 때 Fog로 어둡게 표현하기 위해 사용.
    // ----------------------------------------------------------------------------------------------------------------------------------------------------

    public void SetData()
    {
        //DataController.Instance.gameData.currentSunRotate = round;
        //DataController.Instance.gameData.currentRotateTime = t;
        DataController.Instance.gameData.currentSunRotate = rotationAmount;
    }
    private void PreInitialize()
    {
        //t = 0.0f;
        //round = 0.0f;
        rotationAmount = 0.0f;
    }

    private void Initialize()
    {
        //round = DataController.Instance.gameData.currentSunRotate;
        //t = DataController.Instance.gameData.currentRotateTime;
        //vec = Quaternion.Euler(round, 0, 0);
        rotationAmount = DataController.Instance.gameData.currentSunRotate;
    }

    //private void RoundTime()
    //{
    //    vec = Quaternion.Euler((int)round, 0, 0);
    //    Debug.Log(round);
    //    int time = (int)round / 15;
    //}
    // 텐트와 상호작용시 할 내용
    // 텐트와 상호작용시 -> 로딩 씬 출현 -> 다시 씬을 불러들여와서 쿼터니언의 범위 값이 0이되게한다.
    // get set을 이용하면 되지않을까....?

    // 04/10 씬 생성완료 씬을 설정해서 텐트와 상호작용 시 로딩 씬출력 텍스트 필요
}
