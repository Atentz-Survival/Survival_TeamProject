using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    Button newGame;

    private void Awake()
    {
        newGame = GetComponent<Button>();
        newGame.onClick.AddListener(OnPlayingUI);
    }

    private void OnPlayingUI()
    {
        SceneManager.LoadScene(0);
    }
}
