using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string currentPlayerName;

    public string playerName = null;         //�̸�
    public int playerHp;
    public Vector3 playerPosition;      //��ġ

    public int[] itemNum = null;               //������ ��ȣ
    public int[] itemCount = null;             // ����
    public string[] itemTypes = null;          // ����
    public Vector3 workbenchPosition;   //���۴� ��ġ

    public string toolTag = null;        // ������ �������� �̸�
    public int toolLevel;         // ������ �������� ����

    public int currentDay;         // ��¥ ���� �迭
    public float currentTime;     // �����̼� ������ ���� �ð� ����
}
