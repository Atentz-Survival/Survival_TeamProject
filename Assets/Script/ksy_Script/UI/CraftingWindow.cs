using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;
    Button startCraft;
    Button menuCloseButton; // �ݱ� ��ư �߰�

    public bool canMakeTool = false;

    public Action DontAction;
    public Action DoAction;



    private void Awake()
    {
        startCraft = GameObject.Find("startCraft").GetComponent<Button>();
        menuCloseButton = transform.GetChild(2).GetComponent<Button>();     // ��ư ã��

    }
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        gameObject.SetActive(false);
        player.onMaking += OpenCraftingWindow;
        startCraft.onClick.AddListener(OnMakeTool);
        menuCloseButton.onClick.AddListener(CloseMenu);                     // ��ư ��� �߰�
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
            startCraft.interactable = canMakeTool;  //�Ұ��� ���濡 ���� Ŭ�� ���� ���� fŰ ���������� üũ
       }
    }

    /// <summary>
    /// ��ư �ݱ�� �Լ�
    /// </summary>
    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
