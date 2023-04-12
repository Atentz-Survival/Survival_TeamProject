using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1 = null;
    [SerializeField] Slider slider = null;
    [SerializeField] TextMeshProUGUI text2 = null;

    private float nowTime;              // 현재
    private float startTime;            // 시작
    private float endTime = 6;          // 끝

    void Start()
    {
        nowTime = endTime;
        startTime = Time.time;
        ChargeSlider(0);
        StartCoroutine(ChnageText(1.5f, text1, text2));
    }

    void Update()
    {
        Check_Loading();
    }

    public IEnumerator ChnageText(float time, TextMeshProUGUI i, TextMeshProUGUI j)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        j.color = new Color(j.color.r, j.color.g, j.color.b, 0);

        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / time));
            yield return null;
        }
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / time));
            yield return null;
        }

        while (j.color.a < 1.0f)
        {
            j.color = new Color(j.color.r, j.color.g, j.color.b, j.color.a + (Time.deltaTime / time));
            yield return null;
        }
        j.color = new Color(j.color.r, j.color.g, j.color.b, 1);
    }

    private void Check_Loading()
    {
        nowTime = Time.time - startTime;              // 현재 시간 = 이기능의 시작시간 - 첫동작 시간
        if (nowTime < endTime)                    // 현재시간이 로딩 시간보다 작을경우
        {
            ChargeSlider(nowTime / endTime);    // 슬라이더의 게이지 = 현재시간 / 로딩시간
        }
        else
        {
            FullSlider();                                  // 현재시간이 로딩시간보다 크거나 같을경우
        }
    }

    private void FullSlider()
    {
        ChargeSlider(1);                                  // 슬라이더의 게이지가 풀
        SceneManager.LoadSceneAsync(1);
    }

    private void ChargeSlider(float chargeValue)
    {
        slider.value = chargeValue;                              // 슬라이더값을 송출
    }
}