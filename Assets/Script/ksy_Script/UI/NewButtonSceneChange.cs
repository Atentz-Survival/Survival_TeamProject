using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class NewButtonSceneChange : MonoBehaviour
{
    Button newButton;
    Button LoadGame;

    public Action whatIsYOurNAme;

    private void Awake()
    {
        newButton = transform.GetChild(0).GetComponent<Button>();
        LoadGame = transform.GetChild(1).GetComponent<Button>();
    }
    private void OnEnable()
    {
        newButton.onClick.AddListener(TestFunction);
        LoadGame.onClick.AddListener(TestFunction2);
    }

    private void TestFunction2()
    {
        //로드씬 델리게이트(세이브 값 가져오기)
        DataController.Instance.LoadGameData();
        SceneManager.LoadScene(2);
    }

    void TestFunction()
    {
        DataController.Instance.DeleteSaveFile();
        whatIsYOurNAme?.Invoke();
    }
}
