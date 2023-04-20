using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    SaveDataPanel playerInputName;
    SetUpItem setUpItem;

    string playerName;         //�̸�
    int playerHp;
    float playerPositionX;      //��ġ
    float playerPositionY;
    float playerPositionZ; 
    
    int[] itemCount = null;             // ����
    int[] itemTypes = new int[ItemManager.Instance.itemInventory.ItemTypeArray.Length];        // ����

    float workbenchPositionX;   //���۴� ��ġ
    float workbenchPositionY;
    float workbenchPositionZ;

    ToolItemTag toolTag;        // ������ �������� �̸�
    int toolName;
    int toolLevel;         // ������ �������� ����

    int currentDay;         // ��¥ ���� �迭

    Quaternion quaternion;
    float currentTimeX;     // �����̼� ������ ���� �ð� ����
    float currentTimeY;     // �����̼� ������ ���� �ð� ����
    float currentTimeZ;     // �����̼� ������ ���� �ð� ����

    int dataCount = 3;              //���� ������ ĭ

    const int NotUpdated = -1;      // ������Ʈ �ȵ� ǥ��

    int updatedIndex = NotUpdated;  //���� ������Ʈ �� ������ �ε���

    private void Awake()
    {
        playerInputName = GetComponent<SaveDataPanel>();
        playerInputName.nameData += PlayerGetName;
        setUpItem = FindObjectOfType<SetUpItem>();
    }

    private void PlayerGetName(string obj)
    {
        playerName = obj;
    }

    private void PlayerData(PlayerBase player)
    {
        int newHp = player.HP;
        playerHp = newHp;   // �� ������ ���� ��ŷ�� ���� �ֱ�
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;

        player.GetToolItem(toolTag, toolLevel);
        toolName = (int)toolTag;
    }

    private void CurrentTime(Sunshine timer)
    {
        //�ð� ����
        timer.DayTimeDeli(quaternion);
        currentTimeX = quaternion.x;
        currentTimeY = quaternion.y;
        currentTimeZ = quaternion.z;
    }

    /*int[] intArray = new int[colors.Length];

for (int i = 0; i<colors.Length; i++)
{
    intArray[i] = (int) colors[i];
}*/
    void ItemSave()
    {
        itemCount = ItemManager.Instance.itemInventory.ItemAmountArray;
        for (int i = 0; i < ItemManager.Instance.itemInventory.ItemTypeArray.Length; i++)
        {
            itemTypes[i] = (int)ItemManager.Instance.itemInventory.ItemTypeArray[i];
        }

        //�Ͽ�¡ ����
        workbenchPositionX = setUpItem.transform.position.x;
        workbenchPositionY = setUpItem.transform.position.y;
        workbenchPositionZ = setUpItem.transform.position.z;


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
