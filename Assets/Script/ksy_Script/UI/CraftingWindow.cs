using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;

    public Action DontAction;
    public Action DoAction;
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        gameObject.SetActive(false);
        player.onMaking += OpenCraftingWindow;
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
