using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    TextMeshPro resumeText;
    TextMeshPro saveText;
    TextMeshPro mainText;
    Button resume;
    Button save;
    Button main;

    private void Awake()
    {
        resume = GameObject.Find("Resume").GetComponent<Button>();
        save = GameObject.Find("Save").GetComponent<Button>();
        main = GameObject.Find("Main").GetComponent<Button>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        resume.onClick.AddListener(ResumeGame);
        save.onClick.AddListener(CallSaveMenu);
        main.onClick.AddListener(CallMainMenu);
    }

    private void CallMainMenu()
    {
        SceneManager.LoadScene("StartUI");
    }

    private void CallSaveMenu()
    {
        SceneManager.LoadScene("SaveLoad");
    }

    private void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
