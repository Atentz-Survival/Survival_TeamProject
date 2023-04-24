using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBoardUI : MonoBehaviour
{
    // 세이브보드 버튼 관리용

    Button saveCloseButton;

    private void Awake()
    {
        saveCloseButton = transform.GetChild(2).GetComponent<Button>();
    }

    private void Start()
    {
        saveCloseButton.onClick.AddListener(CloseSaveBoard);
    }

    // 버튼 기능 추가
    private void CloseSaveBoard()
    {
        gameObject.SetActive(false);
    }
}
