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
    public float playerPositionX;      //��ġ
    public float playerPositionY;      //��ġ
    public float playerPositionZ;      //��ġ

    public int[] itemNum = null;               //������ ��ȣ
    public int[] itemCount = null;             // ����
    public string[] itemTypes = null;          // ����

    public float workbenchPositionX;   //���۴� ��ġ
    public float workbenchPositionY;   //���۴� ��ġ
    public float workbenchPositionZ;   //���۴� ��ġ

    public int currentToolItemTag;
    /*public string toolTag = null;        // ������ �������� �̸�*/
    public int toolLevel;         // ������ �������� ����

    public int currentDay;         // ��¥ ���� �迭
    public float currentTime;     // �����̼� ������ ���� �ð� ����


    /*public Action<ToolItemTag, int> GetToolItem;*/
}
