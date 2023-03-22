using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Data1 : MonoBehaviour
{
    Button data1;

    private void Awake()
    {
        data1 = GetComponent<Button>();
        data1.onClick.AddListener(OnPlayingUI);
    }

    private void OnPlayingUI()
    {
        SceneManager.LoadScene(2);
    }
}
