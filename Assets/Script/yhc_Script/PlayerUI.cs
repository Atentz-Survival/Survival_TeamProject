using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    Slider HPUI;
    PlayerBase player;
    Button pauseButton;
    PauseMenu pauseMenu;
    UIAct uikey;
    OptionMenu optionMenu;
    Transform diePanel;
    Button returnMainButton;
    TextMeshProUGUI hpAlarm;
    Transform keySet;
    Transform help;

    float diePanelOpen;
    bool menuClosed;
    bool keySetOn = false;

    private void Awake()
    {
        player = FindObjectOfType<PlayerBase>();
        HPUI = GetComponentInChildren<Slider>();
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        optionMenu = FindObjectOfType<OptionMenu>();
        keySet = transform.GetChild(4);
        diePanel = transform.GetChild(5);
        help = transform.GetChild(6);
        returnMainButton = diePanel.GetComponentInChildren<Button>();

        uikey = new UIAct();
    }



    private void Start()
    {
        diePanelOpen = 0.0f;
        player = FindObjectOfType<PlayerBase>();
        menuClosed = true;
        
        diePanel.gameObject.SetActive(false);
        keySet.gameObject.SetActive(false);

        player.onDie += OneDiePanel;
        pauseButton.onClick.AddListener(CallPauseMenu);
        returnMainButton.onClick.AddListener(CallMainMenu);

        StartCoroutine(OnHelpPanel());
    }

    private void FixedUpdate()
    {
        HPUI.value = player.HP;
    }

    private void OnEnable()
    {
        uikey.UI.Enable();
        uikey.UI.Esc.performed += ESC;
        uikey.UI.KeySet.performed += OnKeySet;

    }


    private void OnDisable()
    {
        uikey.UI.KeySet.performed -= OnKeySet;
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

    private void OneDiePanel()
    {
        float delay = 4.0f;
        diePanelOpen += Time.deltaTime;
        if (diePanelOpen > delay)
        {
            diePanel.gameObject.SetActive(true);
        }
    }

    private void OnKeySet(InputAction.CallbackContext _)
    {
        if (keySetOn == false)
        {
            keySet.gameObject.SetActive(true);
            keySetOn = !keySetOn;
        }
        else
        {
            keySet.gameObject.SetActive(false);
            keySetOn = !keySetOn;
        }
    }

    private void CallMainMenu()
    {
        SceneManager.LoadScene(0);  // 죽고 난 후 main버튼 클릭시 0번씬으로 이동하도록 변경
    }


    IEnumerator OnHelpPanel()
    {
        help.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        help.gameObject.SetActive(false);
        StopCoroutine(OnHelpPanel());
    }

}
