using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    Button newGame;

    private void Awake()
    {
        newGame = GetComponent<Button>();
        newGame.onClick.AddListener(OnSaveLoadUIScene);
    }

    private void OnSaveLoadUIScene()
    {
        SceneManager.LoadScene("SaveLoadUI");
    }
}
