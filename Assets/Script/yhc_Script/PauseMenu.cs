using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    Button resume;
    Button save;
    Button main;
    Button option;
    OptionMenu optionMenu;

    public Action onSave;


    private void Awake()
    {
        resume = GameObject.Find("Resume").GetComponent<Button>();
        save = GameObject.Find("Save").GetComponent<Button>();
        main = GameObject.Find("Main").GetComponent<Button>();
        option = GameObject.Find("Option").GetComponent<Button>();
        optionMenu = GameObject.Find("OptionMenu").GetComponent<OptionMenu>();
    }

    private void Start()
    {
        resume.onClick.AddListener(ResumeGame);
        save.onClick.AddListener(CallSaveMenu);
        main.onClick.AddListener(CallMainMenu);
        option.onClick.AddListener(CallOptionMenu);
        gameObject.SetActive(false);
        optionMenu.gameObject.SetActive(false);
    }

    private void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void CallSaveMenu()
    {
        onSave?.Invoke();
    }

    private void CallMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CallOptionMenu()
    {
        optionMenu.gameObject.SetActive(true);
    }
}
