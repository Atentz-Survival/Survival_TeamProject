using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadDataPanel : MonoBehaviour
{
    Button slot0;
    Button slot1;
    Button slot2;
    Button slot3;

    private void Awake()
    {
        slot0 = transform.GetChild(0).GetComponent<Button>();
        slot1 = transform.GetChild(1).GetComponent<Button>();
        slot2 = transform.GetChild(2).GetComponent<Button>();
        slot3 = transform.GetChild(3).GetComponent<Button>();
    }

    // private void Start()
    // {
    //     slot0.onClick.AddListener(CallSaveData());
    //     slot1.onClick.AddListener(CallSaveData());
    //     slot2.onClick.AddListener(CallSaveData());
    //     slot3.onClick.AddListener(CallSaveData());
    // }

    // private UnityAction CallSaveData()
    // {
    //     
    // }
}
