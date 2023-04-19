using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    SaveDataPanel playerInputName;

    string playerName;         //�̸�
    int playerHp;
    Vector3 playerPosition;      //��ġ

    int[] itemNum = null;               //������ ��ȣ
    int[] itemCount = null;             // ����
    string[] itemTypes = null;          // ����
    Vector3 workbenchPosition;   //���۴� ��ġ

    string toolTag = null;        // ������ �������� �̸�
    int toolLevel;         // ������ �������� ����

    int currentDay;         // ��¥ ���� �迭
    float currentTime;     // �����̼� ������ ���� �ð� ����

    int dataCount = 3;              //���� ������ ĭ

    const int NotUpdated = -1;      // ������Ʈ �ȵ� ǥ��

    int updatedIndex = NotUpdated;  //���� ������Ʈ �� ������ �ε���

    private void Awake()
    {
        playerInputName = GetComponent<SaveDataPanel>();
        playerInputName.nameData += PlayerGetName;
    }

    private void PlayerGetName(string obj)
    {
        playerName = obj;
    }

    private void PlayerData(PlayerBase player)
    {
        int newHp = player.HP;
        playerHp = newHp;   // �� ������ ���� ��ŷ�� ���� �ֱ�
        playerPosition = player.transform.position;
    }


    public void SavePlayerData()
    {
        // ������ �����͸� �����մϴ�.
        SaveData playerData = new SaveData();

        // �����͸� JSON �������� ��ȯ�մϴ�.
        string json = JsonUtility.ToJson(playerData);

        // JSON ������ �����ϰ� �����͸� �����մϴ�.
        string filePath = Application.dataPath + "/Resources/player_data.json";
        File.WriteAllText(filePath, json);
    }
}
