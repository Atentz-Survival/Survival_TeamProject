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
    OptionMenu optionMenu;

    bool menuClosed;


    private void Awake()
    {
        HPUI = GetComponentInChildren<Slider>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        optionMenu = FindObjectOfType<OptionMenu>();
        uikey = new UIAct();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        menuClosed = true;
        pauseButton.onClick.AddListener(CallPauseMenu);
    }

    // 합칠때 다시 살려야됨, HP업데이트
    //private void FixedUpdate()
    //{
    //    HPUI.value = player.HP;
    //}

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
            menuClosed = !menuClosed;
        }
        else if(!menuClosed)
        {
            if(optionMenu.optionClosed == false)
            {
                optionMenu.gameObject.SetActive(false);
            }
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            menuClosed = !menuClosed;
        }
    }

    private void CallPauseMenu()
    {
        if (pauseMenu != null && menuClosed)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            menuClosed = !menuClosed;
        }
        else if( pauseMenu != null && !menuClosed ) 
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            menuClosed = !menuClosed;
        }
        else
        {
            Debug.LogWarning("퍼즈 메뉴 불러오기 실패");
        }
        Debug.Log("퍼즈");
    }
}
