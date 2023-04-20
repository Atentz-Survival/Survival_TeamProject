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

    string playerName;         //이름
    int playerHp;
    float playerPositionX;      //위치
    float playerPositionY;
    float playerPositionZ; 
    
    int[] itemCount = null;             // 갯수
    int[] itemTypes = new int[ItemManager.Instance.itemInventory.ItemTypeArray.Length];        // 종류

    float workbenchPositionX;   //제작대 위치
    float workbenchPositionY;
    float workbenchPositionZ;

    ToolItemTag toolTag;        // 장착한 장비아이템 이름
    int toolName;
    int toolLevel;         // 장착한 장비아이템 레벨

    int currentDay;         // 날짜 저장 배열

    Quaternion quaternion;
    float currentTimeX;     // 로테이션 값으로 현재 시간 저장
    float currentTimeY;     // 로테이션 값으로 현재 시간 저장
    float currentTimeZ;     // 로테이션 값으로 현재 시간 저장

    int dataCount = 3;              //저장 데이터 칸

    const int NotUpdated = -1;      // 업데이트 안됨 표시

    int updatedIndex = NotUpdated;  //현재 업데이트 된 데이터 인덱스

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
        playerHp = newHp;   // 새 점수를 현재 랭킹에 끼워 넣기
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;

        player.GetToolItem(toolTag, toolLevel);
        toolName = (int)toolTag;
    }

    private void CurrentTime(Sunshine timer)
    {
        //시간 저장
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

        //하우징 정보
        workbenchPositionX = setUpItem.transform.position.x;
        workbenchPositionY = setUpItem.transform.position.y;
        workbenchPositionZ = setUpItem.transform.position.z;


    }

    public void SavePlayerData()
    {
        // 저장할 데이터를 생성합니다.
        SaveData playerData = new SaveData();

        // 데이터를 JSON 형식으로 변환합니다.
        string json = JsonUtility.ToJson(playerData);
        // JSON 파일을 생성하고 데이터를 저장합니다.
        string filePath = Application.dataPath + "/Resources/player_data.json";
        File.WriteAllText(filePath, json);
    }
}
