using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string currentPlayerName;

    public string playerName = null;         //이름
    public int playerHp;
    public float playerPositionX;      //위치
    public float playerPositionY;      //위치
    public float playerPositionZ;      //위치

    public int[] itemNum = null;               //아이템 번호
    public int[] itemCount = null;             // 갯수
    public string[] itemTypes = null;          // 종류

    public float workbenchPositionX;   //제작대 위치
    public float workbenchPositionY;   //제작대 위치
    public float workbenchPositionZ;   //제작대 위치

    public int currentToolItemTag;
    /*public string toolTag = null;        // 장착한 장비아이템 이름*/
    public int toolLevel;         // 장착한 장비아이템 레벨

    public int currentDay;         // 날짜 저장 배열
    public float currentTime;     // 로테이션 값으로 현재 시간 저장


    /*public Action<ToolItemTag, int> GetToolItem;*/
}
