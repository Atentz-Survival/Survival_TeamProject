using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[Serializable]
public class GameData
{

    public string playerName;         //�̸�
    public int playerHp;
    public Vector3 playerPosition;      //��ġ

    /*public int[] itemCount;             // ����
    public int[] itemTypes;          // ����
*/
   /* public Vector3 workbenchPosition;   //���۴� ��ġ*/

    public int currentToolItem;
    public int toolLevel;         // ������ �������� ����

    public int currentDay;         // ��¥ ���� �迭
    public int currentTime;

    public float currentSunRotate;
}
