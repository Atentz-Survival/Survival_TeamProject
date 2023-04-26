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
        newButton.onClick.AddListener(StartGameFunction);
        LoadGame.onClick.AddListener(LoadGameFunction);
    }

    private void LoadGameFunction()
    {

        //�ε�� ��������Ʈ(���̺� �� ��������)
        DataController.Instance.LoadGameData();
        SceneManager.LoadScene(2);
    }

    void StartGameFunction()
    {
        DataController.Instance.DeleteSaveFile();
        whatIsYOurNAme?.Invoke();
    }
}
