using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string playerName;         //�̸�
    public int playerHp;
    public float playerPositionX;      //��ġ
    public float playerPositionY;
    public float playerPositionZ;

    public int[] itemCount = null;             // ����
    public int[] itemTypes = null;          // ����

    public float workbenchPositionX;   //���۴� ��ġ
    public float workbenchPositionY;   //���۴� ��ġ
    public float workbenchPositionZ;   //���۴� ��ġ

    public int currentToolItemTag;
    public int toolLevel;         // ������ �������� ����

    public int currentDay;         // ��¥ ���� �迭
    public float currentTimeX;     // �����̼� ������ ���� �ð� ����
    public float currentTimeY;
    public float currentTimeZ;

    /*public Action<ToolItemTag, int> GetToolItem;*/
}
