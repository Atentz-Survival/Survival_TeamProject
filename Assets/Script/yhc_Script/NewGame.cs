using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    Button newGame;

    private void Awake()
    {
        newGame = GetComponent<Button>();
        newGame.onClick.AddListener(OnPlayingUI);
    }

    private void OnPlayingUI()
    {
        SceneManager.LoadScene(2);
    }
}
