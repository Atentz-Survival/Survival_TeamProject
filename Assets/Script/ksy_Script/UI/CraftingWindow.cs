using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        player.onMaking += OpenCraftingWindow;
    }
    private void OnEnable()
    {
    }

    private void OpenCraftingWindow()
    {
       if (gameObject.activeSelf == true)
       {
           gameObject.SetActive(false);
       }
       else
       {
           gameObject.SetActive(true);
       }
    }
}
