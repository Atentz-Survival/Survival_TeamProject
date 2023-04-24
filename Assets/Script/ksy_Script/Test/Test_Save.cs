using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test_Save : MonoBehaviour
{
    Button slot0;
    Button slot1;
    Button slot2;
    Button slot3;
    TMP_InputField inputField;

    public Action<string> nameData;

    private void Awake()
    {
        slot0 = GameObject.Find("SLOT0").GetComponent<Button>();
        slot1 = GameObject.Find("SLOT1").GetComponent<Button>();
        slot2 = GameObject.Find("SLOT2").GetComponent<Button>();
        slot3 = GameObject.Find("SLOT3").GetComponent<Button>();
        inputField = GameObject.Find("PlayerNameSet").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        slot0.onClick.AddListener(InputNamer);
        slot1.onClick.AddListener(InputNamer);
        slot2.onClick.AddListener(InputNamer);
        slot3.onClick.AddListener(InputNamer);
        inputField.onEndEdit.AddListener(InputNames);
    }

    private void InputNamer()
    {
        inputField.gameObject.SetActive(true);
    }

    void InputNames(string text)
    {
        string nameText = text.ToString();
        nameData?.Invoke(nameText);
        inputField.gameObject.SetActive(false);
    }
}
