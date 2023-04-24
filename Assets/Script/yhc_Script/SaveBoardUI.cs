using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBoardUI : MonoBehaviour
{
    // ���̺꺸�� ��ư ������

    Button saveCloseButton;

    private void Awake()
    {
        saveCloseButton = transform.GetChild(2).GetComponent<Button>();
    }

    private void Start()
    {
        saveCloseButton.onClick.AddListener(CloseSaveBoard);
    }

    // ��ư ��� �߰�
    private void CloseSaveBoard()
    {
        gameObject.SetActive(false);
    }
}
