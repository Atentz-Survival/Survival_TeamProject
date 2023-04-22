using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;
    Button startCraft;

    public bool canMakeTool = false;

    public Action DontAction;
    public Action DoAction;



    private void Awake()
    {
        startCraft = GameObject.Find("startCraft").GetComponent<Button>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        gameObject.SetActive(false);
        player.onMaking += OpenCraftingWindow;
        startCraft.onClick.AddListener(OnMakeTool);
    }

    private void FixedUpdate()
    {
        startCraft.interactable = canMakeTool;  //�Ұ��� ���濡 ���� Ŭ�� ���� ���� ��� üũ
    }

    void OnMakeTool()
    {
        if(canMakeTool == true)
        {
            Debug.Log("���� ����");
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
       }
    }
}
