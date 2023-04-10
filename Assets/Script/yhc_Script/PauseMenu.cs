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

    private void Awake()
    {
        resume = GameObject.Find("Resume").GetComponent<Button>();
        save = GameObject.Find("Save").GetComponent<Button>();
        main = GameObject.Find("Main").GetComponent<Button>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // resume.onClick.AddListener(//ResumeGame());
    }

}
