using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    SaveDataPanel playerInputName;

    string playerName;         //이름
    int playerHp;
    Vector3 playerPosition;      //위치

    int[] itemNum = null;               //아이템 번호
    int[] itemCount = null;             // 갯수
    string[] itemTypes = null;          // 종류
    Vector3 workbenchPosition;   //제작대 위치

    string toolTag = null;        // 장착한 장비아이템 이름
    int toolLevel;         // 장착한 장비아이템 레벨

    int currentDay;         // 날짜 저장 배열
    float currentTime;     // 로테이션 값으로 현재 시간 저장

    int dataCount = 3;              //저장 데이터 칸

    const int NotUpdated = -1;      // 업데이트 안됨 표시

    int updatedIndex = NotUpdated;  //현재 업데이트 된 데이터 인덱스

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
        playerHp = newHp;   // 새 점수를 현재 랭킹에 끼워 넣기
        playerPosition = player.transform.position;
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
