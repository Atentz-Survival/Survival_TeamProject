using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;
    Button startCraft;
    Button menuCloseButton; // 닫기 버튼 추가

    public bool canMakeTool = false;

    public Action DontAction;
    public Action DoAction;



    private void Awake()
    {
        startCraft = GameObject.Find("startCraft").GetComponent<Button>();
        menuCloseButton = transform.GetChild(2).GetComponent<Button>();     // 버튼 찾기

    }
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        gameObject.SetActive(false);
        player.onMaking += OpenCraftingWindow;
        startCraft.onClick.AddListener(OnMakeTool);
        menuCloseButton.onClick.AddListener(CloseMenu);                     // 버튼 기능 추가
    }

    void OnMakeTool()
    {
        if(canMakeTool == true)
        {
            Debug.Log("제작 가능");
        }
    }

    private void OpenCraftingWindow()
    {
       if (gameObject.activeSelf == true)
       {
           gameObject.SetActive(false);
            DoAction.Invoke();
       }
       else
       {
           gameObject.SetActive(true);
            DontAction?.Invoke();
            startCraft.interactable = canMakeTool;  //불값의 변경에 따른 클릭 가능 여부 f키 누를때마다 체크
       }
    }

    /// <summary>
    /// 버튼 닫기용 함수
    /// </summary>
    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
