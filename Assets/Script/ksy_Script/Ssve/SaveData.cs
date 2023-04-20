using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string playerName;         //이름
    public int playerHp;
    public float playerPositionX;      //위치
    public float playerPositionY;
    public float playerPositionZ;

    public int[] itemCount = null;             // 갯수
    public int[] itemTypes = null;          // 종류

    public float workbenchPositionX;   //제작대 위치
    public float workbenchPositionY;   //제작대 위치
    public float workbenchPositionZ;   //제작대 위치

    public int currentToolItemTag;
    public int toolLevel;         // 장착한 장비아이템 레벨

    public int currentDay;         // 날짜 저장 배열
    public float currentTimeX;     // 로테이션 값으로 현재 시간 저장
    public float currentTimeY;
    public float currentTimeZ;

    /*public Action<ToolItemTag, int> GetToolItem;*/
}
