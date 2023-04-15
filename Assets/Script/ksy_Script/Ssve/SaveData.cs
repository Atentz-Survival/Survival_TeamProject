using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string[] playerName;         //�̸�
    public float[] playerPosition;      //��ġ

    public int[] itemNum;               //������ ��ȣ
    public int[] itemCount;             // ����
    public string[] itemTypes;          // ����
    public float[] workbenchPosition;   //���۴� ��ġ

    public string[] toolTag;        // ������ �������� �̸�
    public int[] toolLevel;         // ������ �������� ����

    public int[] currentDay;         // ��¥ ���� �迭
    public float[] currentTime;     // �����̼� ������ ���� �ð� ����
}
