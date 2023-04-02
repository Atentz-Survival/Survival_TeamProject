using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    TextMeshProUGUI HPText;
    Slider HPUI;
    PlayerBase player;
    public PauseMenu pauseMenu;
    public Button pauseButton;

    private void Awake()
    {
        player = FindObjectOfType<PlayerBase>();
        HPUI = GetComponentInChildren<Slider>();
        HPText= GetComponentInChildren<TextMeshProUGUI>();
        pauseButton = GetComponent<Button>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        pauseButton.onClick.AddListener(CallPauseMenu);
    }

    private void Start()
    {
    }


    private void FixedUpdate()
    {
        HPUI.value = player.HP;
        HPText.text = $"{player.HP} / 1000";
    }
    private void CallPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        Debug.Log("메뉴 불러오기");
    }
}   
