using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    // TextMeshProUGUI HPText;
    Slider HPUI;
    PlayerBase player;
    public PauseMenu pauseMenu;
    public Button pauseButton;
    // r

    UIAction uiAction;

    private void Awake()
    {
        HPUI = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>();
        pauseButton = GetComponent<Button>();
        uiAction = new UIAction();

        pauseButton.onClick.AddListener(CallPauseMenu);
    }

    private void Start()
    { 
        player = FindObjectOfType<PlayerBase>();
<<<<<<< Updated upstream
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnEnable()
    {
        uiAction.Enable();
        uiAction.UI.Esc.performed += Esc;
    }

    private void Esc(InputAction.CallbackContext _)
    {
        pauseMenu.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        uiAction.UI.Esc.performed -= Esc;
        uiAction.Disable();
=======
        pauseButton = GameObject.Find("Pause").GetComponent<Button>();
        HPUI = GetComponentInChildren<Slider>();
        // HPText = GetComponentInChildren<TextMeshProUGUI>();
        pauseButton.onClick.AddListener(CallPauseMenu);
>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
        HPUI.value = player.HP;
        // HPText.text = $"{player.HP} / 1000";
    }
    private void CallPauseMenu()
    {
<<<<<<< Updated upstream
        pauseMenu.gameObject.SetActive(true);
        Debug.Log("메뉴 불러오기");
=======
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
        Debug.Log("퍼즈");
>>>>>>> Stashed changes
    }
}   
