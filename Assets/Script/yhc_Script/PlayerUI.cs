using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    TextMeshProUGUI HPText;
    Slider HPUI;
    PlayerBase player;
    PauseMenu pauseMenu;
    public Button pauseButton;
    bool IsClosed = true;

    UIAct uiAct;

    private void Awake()
    {
        uiAct = new UIAct();
        pauseMenu = transform.Find("PauseMenu").GetComponent<PauseMenu>();
        int i = 0;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        pauseButton = GameObject.Find("Pause").GetComponent<Button>();
        HPUI = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>();
        pauseButton.onClick.AddListener(CallPauseMenu);
    }

    private void FixedUpdate()
    {
        HPUI.value = player.HP;
        HPText.text = $"{player.HP} / 1000";
    }

    private void CallPauseMenu()
    {
        if(pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(true);
            IsClosed = false;
            Time.timeScale = 0;
        }
        else if(pauseMenu != null && !IsClosed)
        {
            pauseMenu.gameObject.SetActive(true);
            IsClosed = true;
        }
        else
        {
            Debug.Log("퍼즈메뉴 오류");
        }
    }

    private void OnEnable()
    {
        uiAct.UI.Enable();
        uiAct.UI.Esc.performed += Esc;
    }

    private void OnDisable()
    {
        uiAct.UI.Esc.performed -= Esc;
        uiAct.UI.Disable();
    }

    private void Esc(InputAction.CallbackContext _)
    {
        if (IsClosed)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            IsClosed = false;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            IsClosed = true;
        }
    }

}