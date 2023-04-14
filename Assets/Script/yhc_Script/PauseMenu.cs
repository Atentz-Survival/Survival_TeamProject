using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : PlayerUI
{
    Button resume;
    Button save;
    Button main;
    Button option;

    private void Awake()
    {
        resume = GameObject.Find("Resume").GetComponent<Button>();
        save = GameObject.Find("Save").GetComponent<Button>();
        main = GameObject.Find("Main").GetComponent<Button>();
        option = GameObject.Find("Option").GetComponent<Button>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
<<<<<<< Updated upstream
        // resume.onClick.AddListener(//ResumeGame());
    }

=======
        resume.onClick.AddListener(ResumeGame);
        save.onClick.AddListener(CallSaveMenu);
        main.onClick.AddListener(CallMainMenu);
        option.onClick.AddListener(CallOptionMenu);
    }

    private void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void CallMainMenu()
    {
        SceneManager.LoadScene("StartUI");
    }

    private void CallSaveMenu()
    {
        SceneManager.LoadScene("SaveLoad");
    }

    private void CallOptionMenu()
    {
        throw new NotImplementedException();
    }
>>>>>>> Stashed changes
}
