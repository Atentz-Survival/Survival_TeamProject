using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string[] playerName;         //이름
    public float[] playerPosition;      //위치

    public int[] itemNum;               //아이템 번호
    public int[] itemCount;             // 갯수
    public string[] itemTypes;          // 종류
    public float[] workbenchPosition;   //제작대 위치

    public string[] toolTag;        // 장착한 장비아이템 이름
    public int[] toolLevel;         // 장착한 장비아이템 레벨

    public int[] currentDay;         // 날짜 저장 배열
    public float[] currentTime;     // 로테이션 값으로 현재 시간 저장
}
