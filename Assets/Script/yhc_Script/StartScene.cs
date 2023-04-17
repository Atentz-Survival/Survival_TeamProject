using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    Button newGame;
    Button saveLoad;
    SaveData saveData;
    SaveDataPanel dataPanel;

    private void Awake()
    {
        newGame = GameObject.Find("NewGame").GetComponent<Button>();
        saveLoad = GameObject.Find("SaveLoad").GetComponent<Button>();
        dataPanel = GameObject.Find("SaveDataPanel").GetComponent<SaveDataPanel>();
    }

    private void Start()
    {
        newGame.onClick.AddListener(CallDataPanel);
        saveLoad.onClick.AddListener(CallDataPanel);
    }

    private void CallDataPanel()
    {
        dataPanel.gameObject.SetActive(true);
    }




    // SceneManager.LoadScene("TestPlayerScene");
}
