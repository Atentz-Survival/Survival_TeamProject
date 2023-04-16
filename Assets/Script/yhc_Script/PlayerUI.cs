using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    Slider HPUI;
    PlayerBase player;
    Button pauseButton;
    PauseMenu pauseMenu;
    UIAct uikey;

    bool menuClosed;


    private void Awake()
    {
        HPUI = GetComponentInChildren<Slider>();
        pauseButton = GetComponent<Button>();
        uikey = new UIAct();

        pauseButton.onClick.AddListener(CallPauseMenu);
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        menuClosed = true;
    }

    private void FixedUpdate()
    {
        HPUI.value = player.HP;
    }

    private void OnEnable()
    {
        uikey.UI.Enable();
        uikey.UI.Esc.performed += ESC;
    }

    private void OnDisable()
    {
        uikey.UI.Esc.performed -= ESC;
        uikey.UI.Disable();
    }

    private void ESC(InputAction.CallbackContext _)
    {
        if(menuClosed)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    private void CallPauseMenu()
    {
        if (pauseMenu != null && menuClosed)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            menuClosed = false;
        }
        else if( pauseMenu != null && !menuClosed ) 
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            menuClosed = true;
        }
        else
        {
            Debug.LogWarning("퍼즈 메뉴 불러오기 실패");
        }
        Debug.Log("퍼즈");
    }
}
